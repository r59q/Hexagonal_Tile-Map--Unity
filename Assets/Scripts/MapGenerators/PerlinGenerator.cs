using System.Collections;
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
            float[,] heightMap = PerlinNoise.NoiseMap((int)tileMap.size.x, (int)tileMap.size.y, noiseScale, randomness);
            float[,] biomeMap = PerlinNoise.NoiseMap((int)tileMap.size.x, (int)tileMap.size.y, biomeNoiseScale, randomness,true);

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