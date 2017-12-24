using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    /// <summary>
    /// The PerlinGenerator is an exstension of the SimpleGenerator.
    /// It generates a map based off of perlin noise.
    /// </summary>
    public class PerlinGenerator : SimpleGenerator
    {
        [Tooltip("The max height of the terrain")]
        public float maxHeight = 3;

        [Tooltip("How random should it be? Doesnt make much of a difference beyond 50")]
        public float randomness = 15;

        [Tooltip("If noise scales are divisible by 1, they will become completely flat")]
        public float noiseScale = 15.2f;
        [Tooltip("If noise scales are divisible by 1, they will become completely flat")]
        public float biomeNoiseScale = 3.4f;

        [Tooltip("The biomes you want to generate")]
        public BiomeData[] biomes;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            float[,] heightMap = PerlinNoise.NoiseMap((int)tileMap.size.x, (int)tileMap.size.y, noiseScale, randomness);
            float[,] biomeMap = PerlinNoise.NoiseMap((int)tileMap.size.x, (int)tileMap.size.y, biomeNoiseScale, randomness,true);

            SetBiomes(biomeMap);
            SetTileHeights(heightMap);

            InstantiateAll();

        }

        // Internal
        void SetBiomes(float[,] noiseMap)
        {
            
            for (int x = 0; x < tileMap.size.x; x++)
            {
                for (int y = 0; y < tileMap.size.y; y++)
                {
                    Tile currentTile = tileMap.Tile(new Vector2(x, y));
                    float noiseValue = noiseMap[x, y];
                    TileData tileData = Biomes.GetBiomeFromPerlin(noiseValue,biomes);
                    currentTile.SetTileData(tileData);

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
    }
}