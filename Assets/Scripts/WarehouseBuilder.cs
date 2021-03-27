using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseBuilder : MonoBehaviour
{
    GameObject warehouseObject;
    Warehouse warehouse;
    List<GraphicalObject> graphicalObjects;
    List<InteractableObject> interactableObjects;

    public void Initialize(Warehouse warehouse)
    {
        warehouseObject = warehouse.gameObject;
        this.warehouse = warehouse;
        graphicalObjects = new List<GraphicalObject>();
            
    }
    public void CreateFloor(Vector3 position,Vector3 endFloor,Material material)
    {
        Floor floor = new Floor(this.warehouse, position, endFloor, material);
        graphicalObjects.Add(floor);
    }
    public void CreateWall(Vector3 position, Vector3 endWall, Material material,float height)
    {
        Wall wall = new Wall(this.warehouse, position, endWall, material,height);
        graphicalObjects.Add(wall);
    }
    public void CreateShelf(Vector3 position, Vector3 size, Material material, Material packageMaterial, float rotation)
    {
        Shelf shelf = new Shelf(this.warehouse, position, size, material, packageMaterial, rotation);
        graphicalObjects.Add(shelf);
    }
}
