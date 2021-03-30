using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageHandler : MonoBehaviour
{
    public Warehouse warehouse;

    public PackageObject packageObject;

    public PlayerInputs playerInputs;

    public PackageHandler(PackageObject packageObject)
    {
        this.packageObject = packageObject;
    }
    void Start()
    {
        playerInputs = warehouse.Player.GetComponent<PlayerInputs>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInputs.highlightRay.detectedObject && (playerInputs.highlightRay.hit.transform == packageObject.GetTransform()))
        {
            
        }
    }
}
