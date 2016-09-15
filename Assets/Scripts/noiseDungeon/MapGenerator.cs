using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public int seed;

    public int mapWidth;
    public int mapHeight;

    [Range(0, 1)]
    public float threshold;

    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public Vector2 offset;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(seed,mapWidth, mapHeight, noiseScale,octaves,persistance,lacunarity, offset);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.threshold = threshold;
        display.DrawNoiseMap(noiseMap);
    }

    void OnValidate()
    {
        if(mapWidth < 1)
        {
            mapWidth = 1;
        }
        if (mapHeight < 1)
        {
            mapHeight = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves <0)
        {
            octaves = 0;
        }
    }
}
