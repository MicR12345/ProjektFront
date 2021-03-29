using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LP.FPSControllerNewInput
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private InputAction movement;
        [SerializeField] private InputAction turning;
        [SerializeField] private InputAction sprintStart;
        [SerializeField] private InputAction sprintFinish;


        private CharacterController controller = null;

        private Camera cam = null;

        private float camRot = 0;

        public float speed = 5f;
        public float sprintSpeed = 20f;
        public float mouseSensitivity = 100;
        public bool isSprinting;

       

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            cam = Camera.main;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            sprintStart.performed += x => SprintPressed();
            sprintFinish.performed += x => SprintReleased();
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
        private void Move()
        {
            
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
     
    }
}


