using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    [SerializeField]
    public Material FloorMaterial;
    public Material ShelfMaterial;
    public Material PackageMaterial;

    public GameObject Player;

    WarehouseBuilder Builder;

    


    // Start is called before the first frame update
    void Start()
    {
        Builder = gameObject.AddComponent<WarehouseBuilder>();
        Builder.Initialize(this);
        Builder.CreateFloor(new Vector3(0, 0, 0), new Vector3(60, 0, 40),FloorMaterial);
        Builder.CreateWall(new Vector3(0, 0, 0), new Vector3(10, 0, 0),FloorMaterial,5);

        Layout layout = new Layout("I:\\packages.json", "I:\\sectors.json");
        Builder.CreateFromLayout(layout);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
