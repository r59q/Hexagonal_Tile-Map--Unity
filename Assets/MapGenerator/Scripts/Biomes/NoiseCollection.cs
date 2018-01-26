using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// Acts as a container for perlin noises.
    /// </summary>
    public class NoiseCollection
    {
        int width;
        int height;

        float randomness;
        float seasonalScale; 
        float heightScale; 
        float biomeScale;

        float[,] seasonalMap;
        float[,] heightMap;
        float[,] sliceMap;
        float[,] biomeMap;

        /// <summary>
        /// Generates and contains all perlin values at once.
        /// </summary>
        /// <param name="widthParam">The map width.</param>
        /// <param name="heightParam">the map height.</param>
        /// <param name="randomnessParam">Randomness factor.</param>
        /// <param name="seasonalScaleParam">The seasonal scale factor.</param>
        /// <param name="heightScaleParam">The height scale factor.</param>
        /// <param name="biomeScaleParam">The biome scale factor.</param>
        public NoiseCollection(int widthParam, int heightParam, float randomnessParam, float seasonalScaleParam, float heightScaleParam, float biomeScaleParam)
        {
            width = widthParam;
            height = heightParam;

            randomness = randomnessParam;

            seasonalScale = seasonalScaleParam;
            heightScale = heightScaleParam;
            biomeScale = biomeScaleParam;

            seasonalMap = PerlinNoise.MakeNoiseMap(width, height, seasonalScale, randomness, true);
            heightMap = PerlinNoise.MakeNoiseMap(width, height, heightScale, randomness);
            biomeMap = PerlinNoise.MakeNoiseMap(width, height, biomeScale, randomness, true);

            // Make slice map scaling
            sliceMap = PerlinNoise.MakeNoiseMap(width, height, biomeScale, randomness, true);
        }

        /// <summary>
        /// Returns a multidimensional array of height noise values.
        /// </summary>
        /// <returns>An array of noises indexed on the grid.</returns>
        public float[,] HeightMap()
        {
            return heightMap;
        }

        /// <summary>
        /// Returns a multidimensional array of seasonal noise values.
        /// </summary>
        /// <returns>An array of noises indexed on the grid.</returns>
        public float[,] SeasonalMap()
        {
            return seasonalMap;
        }

        /// <summary>
        /// Returns a multidimensional array of biome noise values.
        /// </summary>
        /// <returns>An array of noises indexed on the grid.</returns>
        public float[,] BiomeMap()
        {
            return biomeMap;
        }

        /// <summary>
        /// Returns a multidimensional array of heightslice noise values.
        /// </summary>
        /// <returns>An array of noises indexed on the grid.</returns>
        public float[,] SliceMap()
        {
            return sliceMap;
        }

    }
}