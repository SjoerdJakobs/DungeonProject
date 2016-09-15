﻿using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

    public Renderer textureRenderer;
    public float threshold;

    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float finalMap = noiseMap[x,y];
                if(finalMap < threshold)
                {
                    finalMap = 1;
                }
                else
                {
                    finalMap = 0;
                }
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, finalMap);
            }

        }
        texture.SetPixels(colorMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width,1,height);
    }
}