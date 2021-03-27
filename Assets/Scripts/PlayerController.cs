using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    public class InputRay
    {
        public Ray ray = new Ray();
        public RaycastHit hit;
        public bool detectedObject { get; set; }
    }

    public InputRay highlightRay;


    // Start is called before the first frame update
    void Start()
    {
        highlightRay = new InputRay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        
    }
}
