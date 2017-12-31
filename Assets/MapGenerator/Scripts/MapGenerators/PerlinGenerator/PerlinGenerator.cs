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
        public float seasonalNoiseScale = 15.2f;
        [Tooltip("If noise scales are divisible by 1, they will become completely flat")]
        public float heightNoiseScale = 15.2f;
        [Tooltip("If noise scales are divisible by 1, they will become completely flat")]
        public float biomeNoiseScale = 3.4f;

        [Tooltip("The seasons you want to generate")]
        public SeasonalData[] seasons;
        BiomeData[] biomes;

        protected override void OnInitialized()
        {
            // Generate perlin noise map.
            NoiseCollection noises = new NoiseCollection((int)tileMap.size.x, (int)tileMap.size.y, randomness, seasonalNoiseScale, heightNoiseScale, biomeNoiseScale);

            // Make biome array.
            List<BiomeData> biomeList = new List<BiomeData>();
            foreach (SeasonalData seasonalData in seasons)
            {
                foreach (SeasonalData.HeightSlice slice in seasonalData.heightSlices)
                {
                    foreach (SeasonalData.HeightSlice.SliceData sliceData in slice.sliceBiomes)
                    {
                        if (!biomeList.Contains(sliceData.biomeData))
                        {
                            biomeList.Add(sliceData.biomeData);
                        }
                    }
                }
            }
            biomes = biomeList.ToArray();

            // Set tile data.
            SetTileHeights(noises.HeightMap());
            SetBiomes(noises);

            // Instantiate tiles.
            InstantiateTiles();

        }

        // Internal
        void SetBiomes(NoiseCollection noiseCollection)
        {
            for (int x = 0; x < tileMap.size.x; x++)
            {
                for (int y = 0; y < tileMap.size.y; y++)
                {
                    Tile currentTile = tileMap.Tile(new Vector2(x, y));
                    float seasonNoise = noiseCollection.SeasonalMap()[x, y];
                    float biomeNoise = PerlinNoise.CapNoise(noiseCollection.BiomeMap()[x, y]);

                    for (int i = 0; i < biomes.Length; i++)
                    {
                        BiomeData biomeData = biomes[i];
                        for (int k = 0; k < biomeData.biomeTileData.Length; k++)
                        {
                            int[] sliceIndexes = Biomes.SliceFromPerlin(noiseCollection, this, x, y);

                            BiomeData.BiomeTileData biomeTileData = biomeData.biomeTileData[k];
                            SeasonalData.HeightSlice.SliceData sliceToPlace = seasons[sliceIndexes[0]].heightSlices[sliceIndexes[1]].sliceBiomes[sliceIndexes[2]];
                            SeasonalData.HeightSlice.SliceData[] toLookIn = seasons[sliceIndexes[0]].heightSlices[sliceIndexes[1]].sliceBiomes;

                            float[] thresholds = Biomes.Threshold(k, biomeData, biomes);
                            //Debug.Log(thresholds[0] + " / " + thresholds[1]);


                            if (biomeNoise >= thresholds[0] && biomeNoise <= thresholds[1])
                            {
                                currentTile.SetTileData(biomeTileData.tile);
                            }
                        }
                    }
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