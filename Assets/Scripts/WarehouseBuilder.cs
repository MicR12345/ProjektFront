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
        List<Vector3> uniquePositions = new List<Vector3>();
        foreach (Tuple<Sector,Vector3> item in layout.ShelfPreparation)
        {
            if (!uniquePositions.Contains(new Vector3(item.Item2.x, item.Item2.y, item.Item2.z))) uniquePositions.Add(new Vector3(item.Item2.x, item.Item2.y, item.Item2.z));
        }
        layout.ShelfPreparation.Sort((x, y) => x.Item2.z.CompareTo(y.Item2.z));
        foreach (Vector3 unique in uniquePositions)
        {
            List<Sector> sectors = new List<Sector>();
            foreach (Tuple<Sector,Vector3> item in layout.ShelfPreparation)
            {
                if(unique.y == item.Item2.y && unique.x == item.Item2.x)
                {
                    sectors.Add(item.Item1);
                }
            }
            CreateShelf(new Vector3(unique.x,0,unique.y), new Vector3(2, 1, 1), warehouse.ShelfMaterial, warehouse.PackageMaterial, 0, sectors);
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
    public void CreateShelf(Vector3 position, Vector3 size, Material material, Material packageMaterial, float rotation,List<Sector> sectors)
    {
        Shelf shelf = new Shelf(this.warehouse, position, size, material, packageMaterial, rotation,sectors);
        graphicalObjects.Add(shelf);
    }
}
