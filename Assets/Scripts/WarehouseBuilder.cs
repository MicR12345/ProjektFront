using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class WarehouseBuilder : MonoBehaviour
{
    GameObject warehouseObject;
    Warehouse warehouse;
    List<GraphicalObject> graphicalObjects;

    public void Initialize(Warehouse warehouse)
    {
        warehouseObject = warehouse.gameObject;
        this.warehouse = warehouse;
        graphicalObjects = new List<GraphicalObject>();
            
    }
    public void CreateFromLayout(Layout layout)
    {
        List<Tuple<Sector, Vector3>> Copy = new List<Tuple<Sector, Vector3>>();
        List<Tuple<Sector, Vector3>> Copy2 = (List<Tuple<Sector, Vector3>>)Copy.OrderBy(x => x.Item2.x).GroupBy(x => x.Item2.x);
        foreach (Tuple<Sector, Vector3> tuple in Copy2)
        {
            Debug.Log(tuple.Item2.x + " " + tuple.Item2.z);
        }
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
    public void CreateShelf(Vector3 position, Vector3 size, Material material, Material packageMaterial, float rotation, List<Sector> sector)
    {
        Shelf shelf = new Shelf(this.warehouse, position, size, material, packageMaterial, rotation, sector);
        graphicalObjects.Add(shelf);
    }
}
