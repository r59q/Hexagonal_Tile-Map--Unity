using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public static class PerlinNoise
    {
        public static float[,] NoiseMap(int mapWidth, int mapHeight, float scale, float noiseMapScale)
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


            for (int x = 0 + offsetX; x < offsetX + originalWidth; x++)
            {
                for (int y = 0 + offsetY; y < offsetY + originalHeight; y++)
                {
                    //print("NoiseMap : " + mapWidth +"x" + mapHeight+"\n"+
                    //    "Looking at X:" + x + " Y:" + y);
                    float sampleX = x / scale;
                    float sampleY = y / scale;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);


                    noiseMap[x - offsetX, y - offsetY] = perlinValue;
                }
            }

            return noiseMap;
        }

        public static float[,] NoiseMap(int mapWidth, int mapHeight, float scale, float noiseMapScale, bool safemode)
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


            for (int x = 0 + offsetX; x < offsetX + originalWidth; x++)
            {
                for (int y = 0 + offsetY; y < offsetY + originalHeight; y++)
                {
                    //print("NoiseMap : " + mapWidth +"x" + mapHeight+"\n"+
                    //    "Looking at X:" + x + " Y:" + y);
                    float sampleX = x / scale;
                    float sampleY = y / scale;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    if (safemode)
                    {
                        if (perlinValue < 0)
                        {
                            perlinValue = 0;
                        }
                        if (perlinValue > 1)
                        {
                            perlinValue = 1;
                        }
                    }

                    noiseMap[x - offsetX, y - offsetY] = perlinValue;
                }
            }

            return noiseMap;
        }

    }

}