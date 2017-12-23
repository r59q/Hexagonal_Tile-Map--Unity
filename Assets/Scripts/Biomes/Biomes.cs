using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public static class Biomes
    {

        static bool bugged = false;
        
        /// <summary>
        /// Used for calculating the threshold of a tile in a biome
        /// </summary>
        /// <param name="index">the BiomeData index</param>
        /// <param name="tileIndex">the BiomeTileData index</param>
        /// <param name="biomes">The biomes to calculate from</param>
        /// <returns>[0]: Lower threshold -- [1]: Upper threshold</returns>
        public static float[] Threshold(int index, int tileIndex, BiomeData[] biomes)
        {
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
               // Debug.Log(currentBiome + "@" + biomeLengths[i]);

            }

            for (int k = 0; k < index; k++)
            {
              //  Debug.Log(biomeLengths[k+1]);
                offset += biomeLengths[k];
            }

           // Debug.Log("Index : " + index + " offset: " + offset);

            float minVal = offset;
            float maxVal = offset + biomeLengths[index];

            float difference = maxVal - minVal;
            float margin = difference / 2f;

            float center = minVal + margin;
            Debug.Log("Center: " + center);

            float lower = center - (margin * biomeTileData.threshold);
            float upper = center + (margin * biomeTileData.threshold);

            Debug.Log("L: " + lower + " - U: " + upper + " @ " + biomeTileData.tile + "\n"+ "min: " + minVal + " Max: " + maxVal);

            return new float[] { lower, upper };

            // old code

            /*
            float margin = biomeLengths[index] / 2;
            float center = (centerOffset) + (margin);


            //Debug.Log(biomeLengths[index]);
            float thresholdLower = center - (margin * biomeTileData.threshold);
            float thresholdUpper = center + (margin * biomeTileData.threshold);
            //Debug.Log(thresholdLower + "<-- " + biomeTileData.tile + " -->" + thresholdUpper);

            return new float[] { thresholdLower, thresholdUpper };
            */
        }

        /// <summary>
        /// Used for calculating the total multipliers of all biomes given
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

        public static float GetThresholdFromTile(Tile tile, BiomeData[] biomes)
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