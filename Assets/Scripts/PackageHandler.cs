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
        if (packageObject.package.isSearched)
        {
            packageObject.SearchChangeColor(true);
        }
        else
        {
            packageObject.SearchChangeColor(true);
        }
        if (playerInputs.highlightRay.detectedObject && (playerInputs.highlightRay.hit.transform == packageObject.GetTransform()))
        {
            warehouse.textUI.text = "Name: " + packageObject.package.Name + "\n" +
            "Dimension: " + packageObject.package.Dimensions + "\n" +
            "Position: " + packageObject.package.Position + "\n" +
            "Package\'s number: " + packageObject.package.Number + "\n" +
            "System\'s number: " + packageObject.package.SystemNumber;
        }
        
    }
}
