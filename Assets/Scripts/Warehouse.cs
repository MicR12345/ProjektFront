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

        //Builder.CreateShelf(new Vector3(5,0,5), new Vector3(3, 1, 1), ShelfMaterial, FloorMaterial, 0);
         for (int z = 0; z < 1; z++)
         {
             for (int i = 0; i < 1; i++)
             {
                 Builder.CreateShelf(new Vector3(5 + (z * 5.2f), 0, 5 + i * 3), new Vector3(3, 1, 1), ShelfMaterial, PackageMaterial, 90);
             }
             for (int i = 0; i < 1; i++)
             {
                 Builder.CreateShelf(new Vector3(6.2f + (z * 5.2f), 0, 5 + i * 3), new Vector3(3, 1, 1), ShelfMaterial, PackageMaterial, 270);
             }
         }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
