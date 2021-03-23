using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : GraphicalObject
{
    Vector3 size;
    List<Sector> sectors;

    static readonly float supportSize = 0.10f;
    static readonly float sectorHeight = 0.05f;
    public Shelf(Warehouse warehouse,Vector3 position,Vector3 size,Material material,float rotation) : base(warehouse, position, material)
    {
        this.position = position;
        this.size = size;
        sectors = new List<Sector>();
        graphicalObject.name = "Shelf" + position.x + "," + position.z;
        graphicalObject.transform.RotateAround(position, graphicalObject.transform.up, rotation);
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        sectors.Add(new Sector());
        GenerateMesh();
        UpdateMesh();
    }
    public override void GenerateMesh()
    {
        GenerateSupports((size.y + sectorHeight) * sectors.Count);
        GenerateSectors();
    }
    private void GenerateSupports(float height)
    {
        int startVerts = verts.Count;
        for (int x = 0; x < 2; x++)
        {
            for (int z = 0; z < 2; z++)
            {
                int vertsCount = verts.Count;
                Vector3 leftbottomPosition = position - new Vector3(size.x/2,0,size.z/2);
                verts.Add(leftbottomPosition + new Vector3(x * (size.x - supportSize), position.y, z * (size.z + supportSize)));
                verts.Add(leftbottomPosition + new Vector3(supportSize, position.y, 0) + new Vector3(x * (size.x - supportSize), 0,z*(size.z+supportSize)));
                verts.Add(leftbottomPosition + new Vector3(0, position.y, -supportSize) + new Vector3(x * (size.x - supportSize), 0, z * (size.z + supportSize)));
                verts.Add(leftbottomPosition + new Vector3(supportSize, position.y, -supportSize) + new Vector3(x * (size.x - supportSize), 0, z * (size.z + supportSize)));

                verts.Add(leftbottomPosition + new Vector3(0, position.y + height,0) + new Vector3(x * (size.x - supportSize), 0, z * (size.z + supportSize)));
                verts.Add(leftbottomPosition + new Vector3(supportSize, position.y+height, 0) + new Vector3(x * (size.x - supportSize), 0, z * (size.z + supportSize)));
                verts.Add(leftbottomPosition + new Vector3(0, position.y+ height, -supportSize) + new Vector3(x * (size.x - supportSize), 0, z * (size.z + supportSize)));
                verts.Add(leftbottomPosition + new Vector3(supportSize, position.y+ height, -supportSize) + new Vector3(x * (size.x - supportSize), 0, z * (size.z + supportSize)));

                //Front
                triangles.Add(vertsCount + 3);
                triangles.Add(vertsCount + 2);
                triangles.Add(vertsCount + 7);
                triangles.Add(vertsCount + 2);
                triangles.Add(vertsCount + 6);
                triangles.Add(vertsCount + 7);
                //Back
                triangles.Add(vertsCount + 5);
                triangles.Add(vertsCount + 0);
                triangles.Add(vertsCount + 1);
                triangles.Add(vertsCount + 5);
                triangles.Add(vertsCount + 4);
                triangles.Add(vertsCount + 0);
                //Left
                triangles.Add(vertsCount + 4);
                triangles.Add(vertsCount + 2);
                triangles.Add(vertsCount + 0);
                triangles.Add(vertsCount + 4);
                triangles.Add(vertsCount + 6);
                triangles.Add(vertsCount + 2);
                //Right
                triangles.Add(vertsCount + 1);
                triangles.Add(vertsCount + 3);
                triangles.Add(vertsCount + 5);
                triangles.Add(vertsCount + 3);
                triangles.Add(vertsCount + 7);
                triangles.Add(vertsCount + 5);
            }
        }
        for (int i = startVerts; i < verts.Count; i = i + 4)
        {
            uvs.Add(new Vector2(0, 0));
            uvs.Add(new Vector2(1, 0));
            uvs.Add(new Vector2(0, 0.49f));
            uvs.Add(new Vector2(1, 0.49f));
        }
    }
    private void GenerateSectors()
    {
        int startVerts = verts.Count;
        for (int i = 0; i < sectors.Count; i++)
        {
            int vertsCount = verts.Count;
            Vector3 leftbottomPosition = position - new Vector3(size.x / 2, 0, size.z / 2);
            verts.Add(leftbottomPosition + new Vector3(0, position.y + i * (sectorHeight + size.y),0));
            verts.Add(leftbottomPosition + new Vector3(size.x,position.y + i*(sectorHeight+size.y), 0));
            verts.Add(leftbottomPosition + new Vector3(0, position.y + i * (sectorHeight + size.y), size.z));
            verts.Add(leftbottomPosition + new Vector3(size.x, position.y + i * (sectorHeight + size.y), size.z));

            verts.Add(leftbottomPosition + new Vector3(0, position.y + i * (sectorHeight + size.y) + sectorHeight, 0));
            verts.Add(leftbottomPosition + new Vector3(size.x, position.y + i * (sectorHeight + size.y) + sectorHeight, 0));
            verts.Add(leftbottomPosition + new Vector3(0, position.y + i * (sectorHeight + size.y) + sectorHeight, size.z));
            verts.Add(leftbottomPosition + new Vector3(size.x, position.y + i * (sectorHeight + size.y) + sectorHeight, size.z));

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
        }
        for (int i = startVerts; i < verts.Count; i=i+4)
        {

            uvs.Add(new Vector2(0, 0.52f));
            uvs.Add(new Vector2(1, 0.52f));
            uvs.Add(new Vector2(0, 1));
            uvs.Add(new Vector2(1, 1));
        }
    }
}
public class Sector
{
    int id;
    List<Package> packages;
}