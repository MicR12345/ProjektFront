using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GraphicalObject
{
    public GameObject graphicalObject;
    protected Warehouse warehouse;

    protected MeshFilter meshFilter;
    protected MeshRenderer meshRenderer;
    protected MeshCollider collider;

    protected Material material;

    protected List<Vector3> verts;
    protected List<int> triangles;
    protected List<Vector2> uvs;

    protected Vector3 position;
    public GraphicalObject(Warehouse warehouse, Vector3 position, Material material)
    {
        graphicalObject = new GameObject();
        this.warehouse = warehouse;

        this.material = material;
        graphicalObject.transform.SetParent(this.warehouse.transform);
        graphicalObject.name = "Graphical Object";

        this.position = position;
        this.graphicalObject.transform.localPosition = position;

        meshFilter = graphicalObject.AddComponent<MeshFilter>();
        meshRenderer = graphicalObject.AddComponent<MeshRenderer>();

        GenerateCollider();

        verts = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
    }
    public abstract void GenerateMesh();
    public void UpdateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        meshRenderer.material = material;
        collider.sharedMesh = mesh;        
    }
    public Transform GetTransform()
    {
        return graphicalObject.transform;
    }
    public void GenerateCollider()
    {
        collider = graphicalObject.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.enabled = true;
    }
}
public class Floor : GraphicalObject
{
    protected Vector3 floorEnd;
    public Floor(Warehouse warehouse, Vector3 position, Vector3 floorEnd, Material material)
        : base(warehouse, position, material)
    {
        this.floorEnd = floorEnd;
        graphicalObject.name = "Floor" + position.x + "," + position.z;
        GenerateMesh();
        UpdateMesh();
        //graphicalObject.transform.position = position;
    }
    public override void GenerateMesh()
    {
        verts.Add(Vector3.zero);
        verts.Add(new Vector3(floorEnd.x, 0, 0));
        verts.Add(new Vector3(0, 0,floorEnd.z));
        verts.Add(new Vector3(floorEnd.x, 0,floorEnd.z));
        verts.Add(new Vector3(0,-0.2f,0));
        verts.Add(new Vector3(floorEnd.x, -0.2f, 0));
        verts.Add(new Vector3(0, -0.2f, floorEnd.z));
        verts.Add(new Vector3(floorEnd.x, -0.2f, floorEnd.z));

        triangles.Add(1);
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(3);
        triangles.Add(6);
        triangles.Add(4);
        triangles.Add(5);
        triangles.Add(7);
        triangles.Add(6);
        triangles.Add(5);

        for (int i = 0; i < verts.Count; i++)
        {
            uvs.Add(new Vector2(verts[i].x, verts[i].z));
        }
    }

}
public class Wall : GraphicalObject
{
    protected Vector3 endWall;
    protected float height;
    public Wall(Warehouse warehouse, Vector3 position, Vector3 endWall, Material material, float height)
        : base(warehouse, position, material)
    {
        this.endWall = endWall;
        this.height = height;
        graphicalObject.name = "Wall" + position.x + "," + position.z;
        GenerateMesh();
        UpdateMesh();
    }
    public override void GenerateMesh()
    {
        verts.Add(position);
        verts.Add(new Vector3(endWall.x, 0, endWall.z));
        verts.Add(new Vector3(position.x, height, position.z));
        verts.Add(new Vector3(endWall.x, height, endWall.z));

        triangles.Add(2);
        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(3);
        triangles.Add(2);
        triangles.Add(1);


        for (int i = 0; i < verts.Count; i++)
        {
            uvs.Add(new Vector2(verts[i].x, verts[i].z));
        }
    }

}
public interface InteractableObject
{
    public void OnRaycastHit();
}