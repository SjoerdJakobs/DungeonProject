using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GeneratePlane : MonoBehaviour
{
    public Transform testPoint;

    public int xSize, ySize;

    private Mesh mesh;
    private Vector3[] vertices;
    public corners _corners;

    private int i;

    private void Awake()
    {
        GeneratePlanesList.generatedPlanes.Add(gameObject);
       /* foreach(GameObject G in GeneratePlanesList.generatedPlanes)
        {
            i++;
            print(G + " " + i);
        }*/
        _corners = new corners();
        xSize = Random.Range(5, 15);
        ySize = Random.Range(5, 15);
        Generate();
    }

    public bool checkCollisions(Vector3 Point)
    {
        return (Point.y >= _corners.downLeftCorner.y && Point.y <= _corners.upperLeftCorner.y && Point.x >= _corners.downLeftCorner.x && Point.x <= _corners.downRightCorner.x);
    }

    void Update()
    {
        if (testPoint != null)
        {
            print(checkCollisions(testPoint.position) + " " + gameObject.name);
        }
        //print(_corners.downLeftCorner+ "down left");
        _corners.downLeftCorner = vertices[0] + transform.position;
        //print(_corners.downRightCorner + "down right");
        _corners.downRightCorner = vertices[xSize] + transform.position;
        //print(_corners.upperRightCorner + "upper right");
        _corners.upperRightCorner = vertices[(xSize + 1) * (ySize + 1) - 1] + transform.position;
        //print(_corners.upperLeftCorner + "upper left");
        _corners.upperLeftCorner = vertices[(xSize + 1) * (ySize + 1) - (xSize + 1)] + transform.position;
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
        _corners.downLeftCorner = vertices[0] + transform.position;
        _corners.downRightCorner = vertices[xSize] + transform.position;
        _corners.upperRightCorner = vertices[(xSize + 1) * (ySize + 1) - 1] + transform.position;
        _corners.upperLeftCorner = vertices[(xSize + 1) * (ySize + 1) - (xSize + 1)] + transform.position;
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
        Gizmos.DrawSphere(_corners.downLeftCorner, 0.4f);
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(_corners.upperRightCorner, 0.4f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_corners.downRightCorner, 0.4f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_corners.upperLeftCorner, 0.4f);
    }

    public struct corners
    {
        public Vector3 downLeftCorner;
        public Vector3 downRightCorner;
        public Vector3 upperLeftCorner;
        public Vector3 upperRightCorner;
    }
    
}