using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public static class Biomes
    {

        /// <summary>
        /// Used for calculating the threshold of a tile in a biome.
        /// This is meant for a single set of biomes. 
        /// It does not offer height based biomes.
        /// </summary>
        /// <param name="index">the BiomeData index</param>
        /// <param name="tileIndex">the BiomeTileData index</param>
        /// <param name="biomes">The biomes to calculate from</param>
        /// <returns>[0]: Lower threshold -- [1]: Upper threshold</returns>
        public static float[] Threshold(int index, int tileIndex, BiomeData[] biomes)
        {
            // setting required data
            int biomeCount = biomes.Length;
            float combinedMultipliers = CombinedMultipliers(biomes);
            float offset = 0;
            float[] biomeLengths = new float[biomeCount];
            BiomeData biome = biomes[index];
            BiomeData.BiomeTileData biomeTileData = biome.biomeTileData[tileIndex];

            for (int i = 0; i < biomeCount; i++)
            {
                BiomeData currentBiome = biomes[i];
                biomeLengths[i] = (currentBiome.multiplier / combinedMultipliers);

            }

            for (int k = 0; k < index; k++)
            {
                offset += biomeLengths[k];
            }

            // Use the acquired data to calculate the threshold
            float minVal = offset;
            float maxVal = offset + biomeLengths[index];

            float difference = maxVal - minVal;
            float margin = difference / 2f;
            float center = minVal + margin;

            float lower = center - (margin * biomeTileData.threshold);
            float upper = center + (margin * biomeTileData.threshold);

            return new float[] { lower, upper };

        }

        /// <summary>
        /// Used for calculating the total multipliers of all biomes given.
        /// </summary>
        /// <param name="biomes">Biomes to calculate the combined multipliers from</param>
        /// <returns>The combined multiplier value from all biomes given</returns>
        public static float CombinedMultipliers(BiomeData[] biomes)
        {
            float total = 0;
            foreach (BiomeData data in biomes)
            {
                total += data.multiplier;
            }
            return total;
        }

        /// <summary>
        /// Used for getting the BiomeData from a tile in an array of biomes.
        /// It loops through the biomes and finds a match to the tile you gave.
        /// </summary>
        /// <param name="tile">The tile to look for</param>
        /// <param name="biomes">The set of biomes to look in</param>
        /// <returns>The BiomeData associated with the given tile in the set of biomes.</returns>
        public static BiomeData GetBiomeFromTile(Tile tile, BiomeData[] biomes)
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

        /// <summary>
        /// Returns the modifier of a tile in an array of biomes.
        /// </summary>
        /// <param name="tile">The tile of which you want the threshold modifier</param>
        /// <param name="biomes">The biome array to look in</param>
        /// <returns>The threshold modifer of this tile.</returns>
        public static float ThresholdModifier(Tile tile, BiomeData[] biomes)
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
            Debug.Log("<color=olive>Warning! Couldn't find threshhold</color>\n" +
                "Have you forgot to set the tiledata to a biome tile?");
            return 1;
        }

        /// <summary>
        /// Returns the correct tiledata from an array of biomes calculated from perlin values
        /// </summary>
        /// <param name="noise">The perlin value to calculate the correct TileData from</param>
        /// <param name="biomes">The array of biomes to calculate within</param>
        /// <returns>The correct tiledata associated with the given perlin noise value</returns>
        public static TileData GetBiomeFromPerlin(float noise, BiomeData[] biomes)
        {
            for (int i = 0; i < biomes.Length; i++)
            {
                BiomeData biome = biomes[i];
                for (int k = biome.biomeTileData.Length - 1; k >= 0; k--)
                {
                    BiomeData.BiomeTileData biomeTileData = biome.biomeTileData[k];
                    TileData tileData = biomeTileData.tile;

                    float[] threshholds = Threshold(i, k, biomes);

                    // if the noise value is within the two thresholds return the tile
                    //Debug.Log(biome + " " + threshholds[1]);
                    if (noise <= threshholds[1] && noise >= threshholds[0])
                    {
                        return tileData;
                    }
                }
            }
            return null;
        }

    }
}