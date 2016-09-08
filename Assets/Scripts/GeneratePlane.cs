using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GeneratePlane : MonoBehaviour
{

    public int xSize, ySize;

    private Mesh mesh;
    private Vector3[] vertices;
    public corners _corners;

    private int i;

    private void Awake()
    {
        GeneratePlanesList.generatedPlanes.Add(gameObject);
        foreach(GameObject G in GeneratePlanesList.generatedPlanes)
        {
            //i++;
            //print(G + " " + i);
        }
        _corners = new corners();
        xSize = Random.Range(5, 15);
        ySize = Random.Range(5, 15);
        Generate();
    }

    private void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Plane";

        vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        for (int i = 0, y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                vertices[i] = new Vector3(x, y);

            }
        }
        _corners.downLeftCorner = vertices[0];
        _corners.downRightCorner = vertices[xSize];
        _corners.upperRightCorner = vertices[(xSize + 1) * (ySize + 1) - 1];
        _corners.upperLeftCorner = vertices[(xSize + 1) * (ySize + 1) - (xSize + 1)];
        mesh.vertices = vertices;

        int[] triangles = new int[xSize * ySize * 6];
        for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
        {
            for (int x = 0; x < xSize; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                triangles[ti + 5] = vi + xSize + 2;
            }
        }
        mesh.triangles = triangles;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(_corners.downLeftCorner.x,_corners.downLeftCorner.y, 0) + transform.position, 0.4f);
        Gizmos.DrawSphere(new Vector3(_corners.upperRightCorner.x, _corners.upperRightCorner.y, 0) + transform.position, 0.4f);
        Gizmos.DrawSphere(new Vector3(_corners.downRightCorner.x, _corners.downRightCorner.y, 0) + transform.position, 0.4f);
        Gizmos.DrawSphere(new Vector3(_corners.upperLeftCorner.x, _corners.upperLeftCorner.y, 0) + transform.position, 0.4f);
    }

    public struct corners
    {
        public Vector2 downLeftCorner;
        public Vector2 downRightCorner;
        public Vector2 upperLeftCorner;
        public Vector2 upperRightCorner;
    }
    
}