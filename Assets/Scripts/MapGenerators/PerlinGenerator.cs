using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class PerlinGenerator : SimpleGenerator
    {

        public float maxHeight = 3;

        public float noiseScale = 15.2f;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // prework
            float[,] noiseMap = GetNoiseMap((int)tileMap.size.x, (int)tileMap.size.y, noiseScale);

            InstantiateAll();

            // work
            // give tiles height
            for (int x = 0; x < tileMap.size.x; x++)
            {
                for (int y = 0; y < tileMap.size.y; y++)
                {
                    Tile currentTile = tileMap.Tile(new Vector2(x, y));
                    currentTile.Height(maxHeight * noiseMap[x, y]);
                }
            }

            // debugging
            for (int x = 0; x < noiseMap.GetLength(0); x++)
            {
                for (int y = 0; y < noiseMap.GetLength(1); y++)
                {
                    //print(noiseMap[x, y]);
                }
            }
        }


        float[,] GetNoiseMap(int mapWidth, int mapHeight, float scale)
        {
            float[,] noiseMap = new float[mapWidth, mapHeight];

            if (scale <= 0)
            {
                scale = 0.0001f;
            }

            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    float sampleX = x / scale;
                    float sampleY = y / scale;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    noiseMap[x, y] = perlinValue;
                }
            }

            return noiseMap;
        }

    }
}