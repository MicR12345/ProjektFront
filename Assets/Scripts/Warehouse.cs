using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warehouse : MonoBehaviour
{
    [SerializeField]
    public Material FloorMaterial;
    public Material ShelfMaterial;
    public Material PackageMaterial;

    public GameObject Player;
    private PlayerInputs playerInputs;

    public Text textUI;

    WarehouseBuilder Builder;

    public List<PackageObject> packagesList;
    // Start is called before the first frame update
    void Start()
    {
        playerInputs = Player.GetComponent<PlayerInputs>();
        List<PackageObject> packagesList = new List<PackageObject>();

        Builder = gameObject.AddComponent<WarehouseBuilder>();
        Builder.Initialize(this);
        Builder.CreateFloor(new Vector3(-40, 0, -40), new Vector3(80, 0, 80),FloorMaterial);
        //Builder.CreateWall(new Vector3(0, 0, 0), new Vector3(10, 0, 0),FloorMaterial,5);

        Layout layout = new Layout("I:\\packages.json", "I:\\sectors.json");
        Builder.CreateFromLayout(layout);
    }
    void FixedUpdate()
    {
        if (!playerInputs.highlightRay.detectedObject)
            textUI.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
