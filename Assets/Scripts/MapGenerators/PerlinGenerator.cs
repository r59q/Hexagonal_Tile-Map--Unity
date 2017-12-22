﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class PerlinGenerator : SimpleGenerator
    {

        public float maxHeight = 3;

        public float randomness = 1;

        [Tooltip("If noise scales are divisible by 1, they will become completely flat")]
        public float noiseScale = 15.2f;

        [Tooltip("If noise scales are divisible by 1, they will become completely flat")]
        public float biomeNoiseScale = 3.4f;

        public BiomeData[] biomes;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // prework
            float[,] heightMap = GetNoiseMap((int)tileMap.size.x, (int)tileMap.size.y, noiseScale, randomness);
            float[,] biomeMap = GetNoiseMap((int)tileMap.size.x, (int)tileMap.size.y, biomeNoiseScale, randomness);

            // set biomes before they spawn
            SetBiomes(biomeMap);

            InstantiateAll();

            // work
            // give tiles height
            SetTileHeights(heightMap);


            // debugging
        }

        void SetBiomes(float[,] noiseMap)
        {
            
            for (int x = 0; x < tileMap.size.x; x++)
            {
                for (int y = 0; y < tileMap.size.y; y++)
                {
                    Tile currentTile = tileMap.Tile(new Vector2(x, y));
                    float noiseValue = noiseMap[x, y];

                    TileData tileData = GetBiomeFromPerlin(noiseValue);
                    currentTile.SetTileData(tileData);

                }
            }
        }

        TileData GetBiomeFromPerlin(float noise)
        {
            int biomeCount = biomes.Length;

            float perBiomeLength = 1f / biomeCount;

            float combinedMultipliers = CombinedMultipliers();

            if (noise < 0)
            {
                noise = 0;
            }
            if (noise > 1)
            {
                noise = 1;
            }
            float centerOffset = 0;
            for (int i = 0; i < biomeCount; i++)
            {
                BiomeData biome = biomes[i];
                float minValue = perBiomeLength * i;

                //print(centerOffset);

                float thisBiomeLenght = 1 * (biome.multiplier / combinedMultipliers);

                //print(biome+"@"+thisBiomeLenght);

                for (int k= biome.biomeTileData.Length-1; k >= 0; k--)
                {
                    BiomeData.BiomeTileData biomeTileData = biome.biomeTileData[k];
                    TileData tileData = biomeTileData.tile;
                    //float minVal = perBiomeLength*i;
                    float margin = perBiomeLength / biomeCount;
                    float center = (perBiomeLength*(i)) +(margin);

                    float thresholdLower = center - (margin * biomeTileData.threshold);
                    float thresholdUpper = center + (margin * biomeTileData.threshold);

                    if (noise <= thresholdUpper && noise >= thresholdLower)
                    {
                        return tileData;
                    }
                }
                centerOffset += thisBiomeLenght;

            }
            print(noise);
            return null;
        }

        float CombinedMultipliers()
        {
            float total = 0;
            foreach (BiomeData data in biomes)
            {
                total += data.multiplier;
            }
            return total;
        }

        BiomeData GetBiomeFromTile(Tile tile)
        {
            foreach (BiomeData biome in biomes)
            {
                foreach (BiomeData.BiomeTileData data in biome.biomeTileData)
                {
                    if (data.tile == tile.GetTileData())
                    {
                        return biome;
                    }
                }
            }
            return null;
        }

        float GetThresholdFromTile(Tile tile)
        {
            foreach (BiomeData biome in biomes)
            {
                foreach (BiomeData.BiomeTileData data in biome.biomeTileData)
                {
                    if (data.tile == tile.GetTileData())
                    {
                        return data.threshold;
                    }
                }
            }
            print("<color=olive>Warning! Couldn't find threshhold</color>\n"+
                "Have you forgot to set the tiledata to a biome tile?");
            return 1;
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