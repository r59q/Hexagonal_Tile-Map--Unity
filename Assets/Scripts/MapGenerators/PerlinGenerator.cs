using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class PerlinGenerator : SimpleGenerator
    {

        public float maxHeight = 3;

        public float randomness = 1;
        public float noiseScale = 15.2f;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // prework
            float[,] noiseMap = GetNoiseMap((int)tileMap.size.x, (int)tileMap.size.y, noiseScale, randomness);

            InstantiateAll();

            // work
            // give tiles height
            SetTileHeights(noiseMap);

            // debugging
            for (int x = 0; x < noiseMap.GetLength(0); x++)
            {
                for (int y = 0; y < noiseMap.GetLength(1); y++)
                {
                    //print(noiseMap[x, y]);
                }
            }
        }

        void SetTileHeights(float[,] noiseMap)
        {
            for (int x = 0; x < tileMap.size.x; x++)
            {
                for (int y = 0; y < tileMap.size.y; y++)
                {
                    Tile currentTile = tileMap.Tile(new Vector2(x, y));
                    currentTile.Height(maxHeight * noiseMap[x, y]);
                }
            }
        }

        float[,] GetNoiseMap(int mapWidth, int mapHeight, float scale, float noiseMapScale)
        {
            if (noiseMapScale < 1)
            {
                noiseMapScale = 1;
            }

            int originalWidth = mapWidth;
            int originalHeight = mapHeight;

            mapWidth = Mathf.FloorToInt(originalWidth * noiseMapScale);
            mapHeight = Mathf.FloorToInt(originalHeight * noiseMapScale);

            float[,] noiseMap = new float[mapWidth, mapHeight];

            if (scale <= 0)
            {
                scale = 0.0001f;
            }

            int offsetX = Random.Range(0, mapWidth - originalWidth);
            int offsetY = Random.Range(0, mapHeight - originalHeight);


            for (int x = 0 + offsetX; x < offsetX+originalWidth; x++)
            {
                for (int y = 0 + offsetY; y < offsetY+originalHeight; y++)
                {
                    //print("NoiseMap : " + mapWidth +"x" + mapHeight+"\n"+
                    //    "Looking at X:" + x + " Y:" + y);
                    float sampleX = x / scale;
                    float sampleY = y / scale;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    noiseMap[x- offsetX, y- offsetY] = perlinValue;
                }
            }

            return noiseMap;
        }

    }
}