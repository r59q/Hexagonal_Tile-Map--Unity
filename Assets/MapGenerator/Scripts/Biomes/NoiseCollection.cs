using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
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
        float[,] biomeMap;

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
        }

        public float[,] HeightMap()
        {
            return heightMap;
        }

        public float[,] SeasonalMap()
        {
            return seasonalMap;
        }

        public float[,] BiomeMap()
        {
            return biomeMap;
        }

    }
}