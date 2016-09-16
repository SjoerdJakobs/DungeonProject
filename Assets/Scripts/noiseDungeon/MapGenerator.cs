using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour {


    public enum DrawMode { NoiseMap, ColourMap, terrainMesh, DungeonMap, dungeonMesh, infiniteDungeonMesh};
    public DrawMode drawMode;

    public int seed;

    public const int mapChunkSize = 241;

    public int mapWidth;
    public int mapHeight;

    public float multiplier;

    [Range(0, 1)]
    public float threshold;

    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    [SerializeField]
    private GameObject _leftWall;
    [SerializeField]
    private GameObject _rightWall;
    [SerializeField]
    private GameObject _upperWall;
    [SerializeField]
    private GameObject _downWall;
    
    public void GenerateButton()
    {
        seed = Random.Range(-100000, 100000);
        GenerateMap();
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(seed, mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity, offset);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.threshold = threshold;

        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        _upperWall.transform.position = new Vector3(0,0,mapHeight/2 - 1);
        _upperWall.transform.localScale = new Vector3(mapWidth + 1, multiplier * 2, 2.5f);
        _downWall.transform.position = new Vector3(0,0,-mapHeight/2 + 1);
        _downWall.transform.localScale = new Vector3(mapWidth + 1, multiplier * 2, 2.5f);
        _leftWall.transform.position = new Vector3(-mapWidth/2 + 1,0,0);
        _leftWall.transform.localScale = new Vector3(2.5f, multiplier * 2, mapHeight + 1);
        _rightWall.transform.position = new Vector3(mapWidth/2 - 1, 0,0);
        _rightWall.transform.localScale = new Vector3(2.5f, multiplier * 2, mapHeight + 1);

        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColourMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.terrainMesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap,multiplier), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.DungeonMap)
        {
            display.DrawNoiseDungeon(noiseMap);
        }
        else if (drawMode == DrawMode.dungeonMesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap,multiplier,threshold), TextureGenerator.TextureFromColourMap(colourMap, mapWidth, mapHeight));
        }
        else if (drawMode == DrawMode.infiniteDungeonMesh)
        {

        }
    }

    void OnValidate()
    {
        if (mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color colour;
}