using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package
{
    public string Name;
    public Vector3 Dimensions;
    public Vector3 Position;
    public int SystemNumber;
    public int Specimen;
    public int Number;
    public int articleCode;
    public Package(string name,Vector3 dimensions,Vector3 position,int systemNumber,int number)
    {
        Name = name;
        Dimensions = dimensions;
        Position = position;
        SystemNumber = systemNumber;
        Number = number;
    }
    
}
public class PackageObject : GraphicalObject,InteractableObject
{
    Shelf shelf;
    public Package package;
    float sectorHeight;
    PackageHandler packageHandler;
    public PackageObject(Warehouse warehouse, Shelf shelf ,float sectorHeight ,Package package, Material material) : base(warehouse, package.Position, material)
    {
        this.package = package;
        this.sectorHeight = sectorHeight;
        this.shelf = shelf;

        this.package.Position.y = +this.sectorHeight;

        graphicalObject.transform.SetParent(shelf.GetTransform());
        graphicalObject.transform.localPosition = this.package.Position;
        graphicalObject.name = "\"" + this.package.Name + "\": " + this.package.SystemNumber;
        packageHandler = graphicalObject.AddComponent<PackageHandler>();
        packageHandler.packageObject = this;
        packageHandler.warehouse = warehouse;

        graphicalObject.layer = 0;

        GenerateMesh();
        UpdateMesh();
    }
    public override void GenerateMesh()
    {
        int vertsCount = verts.Count;
        Vector3 leftbottomPosition = new Vector3(- shelf.GetSize().x/2f,Shelf.sectorHeight,- shelf.GetSize().z / 2f);
        verts.Add(leftbottomPosition);
        verts.Add(leftbottomPosition + new Vector3(package.Dimensions.x,0,0));
        verts.Add(leftbottomPosition + new Vector3(0, 0, package.Dimensions.z));
        verts.Add(leftbottomPosition + new Vector3(package.Dimensions.x, 0, package.Dimensions.z));

        leftbottomPosition.y = +package.Dimensions.y;
        verts.Add(leftbottomPosition);
        verts.Add(leftbottomPosition + new Vector3(package.Dimensions.x, 0, 0));
        verts.Add(leftbottomPosition + new Vector3(0, 0, package.Dimensions.z));
        verts.Add(leftbottomPosition + new Vector3(package.Dimensions.x, 0, package.Dimensions.z));

        //Front
        triangles.Add(vertsCount + 7);
        triangles.Add(vertsCount + 2);
        triangles.Add(vertsCount + 3);
        triangles.Add(vertsCount + 7);
        triangles.Add(vertsCount + 6);
        triangles.Add(vertsCount + 2);
        //Back
        triangles.Add(vertsCount + 1);
        triangles.Add(vertsCount + 0);
        triangles.Add(vertsCount + 5);
        triangles.Add(vertsCount + 0);
        triangles.Add(vertsCount + 4);
        triangles.Add(vertsCount + 5);
        //Left
        triangles.Add(vertsCount + 0);
        triangles.Add(vertsCount + 2);
        triangles.Add(vertsCount + 4);
        triangles.Add(vertsCount + 2);
        triangles.Add(vertsCount + 6);
        triangles.Add(vertsCount + 4);
        //Right
        triangles.Add(vertsCount + 5);
        triangles.Add(vertsCount + 3);
        triangles.Add(vertsCount + 1);
        triangles.Add(vertsCount + 5);
        triangles.Add(vertsCount + 7);
        triangles.Add(vertsCount + 3);
        //Bottom
        triangles.Add(vertsCount + 3);
        triangles.Add(vertsCount + 2);
        triangles.Add(vertsCount + 1);
        triangles.Add(vertsCount + 2);
        triangles.Add(vertsCount + 0);
        triangles.Add(vertsCount + 1);
        //Top
        triangles.Add(vertsCount + 6);
        triangles.Add(vertsCount + 7);
        triangles.Add(vertsCount + 5);
        triangles.Add(vertsCount + 6);
        triangles.Add(vertsCount + 5);
        triangles.Add(vertsCount + 4);

        for (int i = 0; i < verts.Count; i++)
        {
            uvs.Add(new Vector2(verts[i].x, verts[i].z));
        }
    }
    public void OnRaycastHit()
    {
        throw new System.NotImplementedException();
    }
    void Update()
    {

    }

}
