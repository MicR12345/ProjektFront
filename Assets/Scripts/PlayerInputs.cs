using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Networking;
using System.Net;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction turning;
    [SerializeField] private InputAction sprintStart;
    [SerializeField] private InputAction sprintFinish;
    
   

    [System.Serializable]
    public class InputRay
    {
        public Ray ray = new Ray();
        public RaycastHit hit;
        public bool detectedObject { get; set; }
    }

    private CharacterController controller;

    private Camera cam;

    public Warehouse warehouse;

    private float camRot = 0;

    public float speed = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100;

    public float sprintSpeed = 20f;
    public bool isSprinting;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public InputRay highlightRay;

    Vector3 velocity;
    bool isGrounded;

    bool isactiveWindow;

    public GameObject UI_form;
    public GameObject textSN;
    public GameObject textSpec;
    public GameObject textPCK;
    TMP_InputField SN;
    TMP_InputField Spec;
    TMP_InputField PCK;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sprintStart.performed += x => SprintPressed();
        sprintFinish.performed += x => SprintReleased();

        cam = Camera.main;
        highlightRay = new InputRay();
        SN = textSN.GetComponent<TMP_InputField>();
        Spec = textSpec.GetComponent<TMP_InputField>();
        PCK = textPCK.GetComponent<TMP_InputField>();
    }


    private void OnEnable()
    {
        movement.Enable();
        turning.Enable();
        sprintStart.Enable();
        sprintFinish.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        turning.Disable();
        sprintStart.Disable();
        sprintFinish.Disable();
    }
    private void SprintPressed()
    {
        isSprinting = true;
    }
    private void SprintReleased()
    {
        isSprinting = false;
    }
    private void Update()
    {
        Move();
        Turn();
    }
    private void FixedUpdate()
    {
        highlightRay.ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        highlightRay.detectedObject = Physics.Raycast(highlightRay.ray, out highlightRay.hit, 7);
        if (highlightRay.detectedObject) Debug.DrawLine(highlightRay.ray.origin, highlightRay.hit.point);
    }
    private void OnDrawGizmos()
    {
        
    }
    private void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        float x = movement.ReadValue<Vector2>().x;
        float z = movement.ReadValue<Vector2>().y;


        Vector3 direction = transform.right * x + transform.forward * z;
        if (isSprinting)
        {
            controller.Move(direction * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(direction * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Turn()
    {
        float mouseX = turning.ReadValue<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = turning.ReadValue<Vector2>().y * mouseSensitivity * Time.deltaTime;

        camRot -= mouseY;
        camRot = Mathf.Clamp(camRot, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(camRot, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }
    private void OnOpenWindow()
    {
        UI_form.SetActive(true);
        movement.Disable();
        turning.Disable();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    private void OnCloseWindow()
    {
        UI_form.SetActive(false);
        movement.Enable();
        turning.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SN.text = "";
        Spec.text = "";
        PCK.text = "";

    }
    private void SendData()
    {
        string text = "https://localhost:44374/api/Package/GetPackage?";
        if(SN.text != "")
        {
            text = text +"systemNumber="+ SN.text;
            if (PCK.text != "" || Spec.text != "")
            {
                text += "&";
            }
        }
        if (Spec.text != "")
        {
            text = text + "specimen=" + Spec.text;
            if(PCK.text != "")
            {
                text += "&";
            }
        }
        if (PCK.text != "")
        {
            text = text + "package=" + PCK.text;
        }
        string packageString = GetStringFromUrl(text);
        if (packageString != "")
        {
            packageString = "{\n\"packageobjects\":" + packageString + "}";
            PackageWrapper packageobjects = JsonUtility.FromJson<PackageWrapper>(packageString);
            foreach (PackageObject item in warehouse.packagesList)
            {
                if (item.package.SystemNumber == packageobjects.packageobjects[0].systemNumber && item.package.Specimen == packageobjects.packageobjects[0].specimen && item.package.Number == packageobjects.packageobjects[0].package)
                {
                    item.package.isSearched = true;
                }
            }
        }
        else Debug.LogError("Server Returned Nothing");
    }
    private string GetStringFromUrl(string url)
    {
        WebClient webClient = new WebClient();
        string result = webClient.DownloadString(url);
        return result;
    }
    [Serializable]
    public class PackageWrapper
    {
        public List<PackageJSON> packageobjects;
    }
    [Serializable]
    public class PackageJSON
    {
        public int systemNumber;
        public int specimen;
        public int package;
        public string articleCode;
        public Dimensions dimensions;
        public Location location;
        [Serializable]
        public class Dimensions
        {
            public float height;
            public float width;
            public float depth;
        }
        [Serializable]
        public class Location
        {
            public string arrangement;
            public int id;
        }
    }
}


