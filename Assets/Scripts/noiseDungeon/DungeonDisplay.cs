using UnityEngine;
using System.Collections;

public class DungeonDisplay : MonoBehaviour {
    
    private CreatePlane createPlaneInfo;
    private GameObject mapGeneratorObject;
    private MapGenerator mapGenerator;
    private Mesh plane;

    void Start()
    {
        mapGeneratorObject = GameObject.Find("MapGenerator");
        createPlaneInfo = GetComponent<CreatePlane>();
        mapGenerator = mapGeneratorObject.GetComponent<MapGenerator>();
        plane = this.GetComponent<MeshFilter>().mesh;
        FormMesh();
    }
    void FormMesh()
    { 
        float[,] noiseMap = Noise.GenerateNoiseMap(mapGenerator.seed,
            createPlaneInfo.xSize, createPlaneInfo.zSize,
            mapGenerator.noiseScale, mapGenerator.octaves, 
            mapGenerator.persistance, mapGenerator.lacunarity, 
            new Vector2(transform.position.x/100, transform.position.z/100));

        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        int vertexIndex = 0;
        
        Vector3[] newVertices = plane.vertices;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float finalMap = noiseMap[x, y];
                if (finalMap < mapGenerator.threshold)
                {
                    finalMap = 1;
                }
                else
                {
                    finalMap = 0.000000001f;
                }

                newVertices[vertexIndex].y = finalMap * 3;

                vertexIndex++;
            }

        }
        plane.vertices = newVertices;
        plane.RecalculateBounds();
        plane.RecalculateNormals();
        //this.gameObject.AddComponent<MeshCollider>();
        print("done in " + Time.realtimeSinceStartup);
    }
    void Update()
    {
        if(mapGenerator.autoUpdate)
        {
            FormMesh();
        }
    }
}
