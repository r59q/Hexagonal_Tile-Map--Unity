using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// Contains the functionality of biomes.
    /// </summary>
    public static class Biomes
    {

        /// <summary>
        /// Used for calculating the threshold of a tile in a biome.
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

            float lower = center - (margin * biomeTileData.multiplier);
            float upper = center + (margin * biomeTileData.multiplier);

            return new float[] { lower, upper };

        }

        /// <summary>
        /// Used for calculating the threshold of a tile in a biome.
        /// </summary>
        /// <param name="index">BiomeTileData index of the tile you wish to analyse.</param>
        /// <param name="biome">The biome(BiomeData) to look in.</param>
        /// <param name="biomes">The set of biomes to look in.</param>
        /// <returns>[0]: Lower threshold -- [1]: Upper threshold</returns>
        public static float[] Threshold(int index, BiomeData biome, BiomeData[] biomes)
        {
            float upper = 0;
            float lower = 0;
            float biomeOffset = 0;
            float combinedBiomeMultipliers = CombinedMultipliers(biomes);

            for (int k = 0; k < biomes.Length; k++)
            {
                float minVal = biomeOffset;
                float maxVal = biomeOffset + (biomes[k].multiplier / combinedBiomeMultipliers);
                float difference = maxVal - minVal;

                float tileOffset = 0;
                for (int i = 0; i < biome.biomeTileData.Length; i++)
                {
                    BiomeData.BiomeTileData currentBiomeTileData = biome.biomeTileData[i];
                    BiomeData.BiomeTileData searchingBiomeTileData = biome.biomeTileData[index];

                    float combinedBiomeTileMultipliers = CombinedMultipliers(biome);

                    upper = tileOffset + ((currentBiomeTileData.multiplier / combinedBiomeTileMultipliers) * difference) + minVal;
                    lower = tileOffset + minVal;

                    if (currentBiomeTileData == searchingBiomeTileData && biome == biomes[k])
                    {
                        return new float[] { lower, upper };
                    }
                    tileOffset += (currentBiomeTileData.multiplier / combinedBiomeTileMultipliers) * difference;
                }
                biomeOffset += (biomes[k].multiplier / combinedBiomeMultipliers);
            }
            throw new System.Exception("Threshold wasn't within the confines of 0-1");
        }

        /// <summary>
        /// Used for calculating the threshold of slices in heightslices.
        /// </summary>
        /// <param name="sliceDataToLookFor">The slice data you wish to calculate.</param>
        /// <param name="sliceDatas">The sliceData array you wish to calculate from.</param>
        /// <returns>[0]: Lower threshold -- [1]: Upper threshold</returns>
        public static float[] Threshold(SeasonalData.HeightSlice.SliceData sliceDataToLookFor, SeasonalData.HeightSlice.SliceData[] sliceDatas)
        {
            float sliceOffset = 0;
            float combinedSliceMultipliers = CombinedMultipliers(sliceDatas);

            for (int k = 0; k < sliceDatas.Length; k++)
            {
                SeasonalData.HeightSlice.SliceData currentSlice = sliceDatas[k];
                float minVal = sliceOffset;
                float maxVal = sliceOffset + currentSlice.multiplier / combinedSliceMultipliers;


                if (currentSlice == sliceDataToLookFor)
                {
                    return new float[] { minVal, maxVal };
                }

                sliceOffset += currentSlice.multiplier / combinedSliceMultipliers;
            }
            throw new System.Exception("Threshold wasn't within the confines of 0-1");
        }

        /// <summary>
        /// Returns the seasonal data associated with a perlin noise from an array of SeasonalData.
        /// </summary>
        /// <param name="noise">The noise from which you want the associated SeasonalData.</param>
        /// <param name="seasons">The array of SeasonalData you wish to calculate from.</param>
        /// <returns>The seasonal data associated with the given perlin noise.</returns>
        public static SeasonalData SeasonFromNoise(float noise, SeasonalData[] seasons)
        {

            for (int i = 0; i < seasons.Length; i++)
            {
                SeasonalData currentSeason = seasons[i];
                float max = Offset(i, seasons) + (currentSeason.multiplier / CombinedMultipliers(seasons));
                float min = Offset(i, seasons);

                if (noise <= max && noise >= min)
                {
                    return currentSeason;
                }

            }

            throw new System.Exception("Data not found in data set!");
        }

        /// <summary>
        /// Returns the heightslice associated with a given perlin noise, calculated from an array of heightslices.
        /// </summary>
        /// <param name="noise">The perlin noise you wish to know the associated heightslice of.</param>
        /// <param name="heightSlices">The array of heightslices you wish to calculate within.</param>
        /// <returns>The heightslice associated with the given perlin noise.</returns>
        public static SeasonalData.HeightSlice HeightSliceFromNoise(float noise, SeasonalData.HeightSlice[] heightSlices)
        {

            for (int i = 0; i < heightSlices.Length; i++)
            {
                SeasonalData.HeightSlice currentHeightSlices = heightSlices[i];
                float max = Offset(i, heightSlices) + (currentHeightSlices.multiplier / CombinedMultipliers(heightSlices));
                float min = Offset(i, heightSlices);

                if (noise <= max && noise >= min)
                {
                    return currentHeightSlices;
                }

            }

            throw new System.Exception("Data not found in data set!");
        }

        /// <summary>
        /// Returns the SliceData associated with a given perlin noise, calculated from an array of SliceData.
        /// </summary>
        /// <param name="noise">The perlin noise you wish to know the associated SliceData of.</param>
        /// <param name="heightSlices">The array of SliceData you wish to calculate within.</param>
        /// <returns>The SliceData associated with the given perlin noise.</returns>
        public static SeasonalData.HeightSlice.SliceData SliceDataFromNoise(float noise, SeasonalData.HeightSlice.SliceData[] slices)
        {

            for (int i = 0; i < slices.Length; i++)
            {
                SeasonalData.HeightSlice.SliceData currentSlice = slices[i];
                float max = Offset(i, slices) + (currentSlice.multiplier / CombinedMultipliers(slices));
                float min = Offset(i, slices);

                if (noise <= max && noise >= min)
                {
                    return currentSlice;
                }

            }

            throw new System.Exception("Data not found in data set!");
        }

        /// <summary>
        /// Returns the TileData associated with a given perlin noise, calculated from a BiomeData set.
        /// </summary>
        /// <param name="noise">The perlin noise you wish to know the associated TileData of.</param>
        /// <param name="heightSlices">The BiomeData you wish to calculate within.</param>
        /// <returns>The TileData associated with the given perlin noise.</returns>
        public static TileData TileDataFromNoise(float noise, BiomeData biomeData)
        {

            for (int i = 0; i < biomeData.biomeTileData.Length; i++)
            {
                TileData currentTile = biomeData.biomeTileData[i].tile;
                float max = Offset(i, biomeData) + (biomeData.biomeTileData[i].multiplier / CombinedMultipliers(biomeData));
                float min = Offset(i, biomeData);

                if (noise <= max && noise >= min)
                {
                    return currentTile;
                }

            }

            throw new System.Exception("Data not found in data set!");
        }

        /// <summary>
        /// Calculate the perlin noise offset based on an index and an array.
        /// </summary>
        /// <param name="i">The index you are currently on.</param>
        /// <param name="seasons">The seasons you are looping through.</param>
        /// <returns>The perlin noise offset.</returns>
        static float Offset(int i, SeasonalData[] seasons)
        {
            float combinedMultipliers = CombinedMultipliers(seasons);
            float offset = 0;
            for (int k = 0; k < i; k++)
            {
                SeasonalData currentSeason = seasons[k];
                offset += currentSeason.multiplier / combinedMultipliers;
            }
            return offset;
        }

        /// <summary>
        /// Calculate the perlin noise offset based on an index and an array.
        /// </summary>
        /// <param name="i">The index you are currently on.</param>
        /// <param name="seasons">The heightslices you are looping through.</param>
        /// <returns>The perlin noise offset.</returns>
        static float Offset(int i, SeasonalData.HeightSlice[] heightSlices)
        {
            float combinedMultipliers = CombinedMultipliers(heightSlices);
            float offset = 0;
            for (int k = 0; k < i; k++)
            {
                SeasonalData.HeightSlice currentHeightSlices = heightSlices[k];
                offset += currentHeightSlices.multiplier / combinedMultipliers;
            }
            return offset;
        }

        /// <summary>
        /// Calculate the perlin noise offset based on an index and an array.
        /// </summary>
        /// <param name="i">The index you are currently on.</param>
        /// <param name="seasons">The SliceData array you are looping through.</param>
        /// <returns>The perlin noise offset.</returns>
        static float Offset(int i, SeasonalData.HeightSlice.SliceData[] slices)
        {
            float combinedMultipliers = CombinedMultipliers(slices);
            float offset = 0;
            for (int k = 0; k < i; k++)
            {
                SeasonalData.HeightSlice.SliceData currentSlice = slices[k];
                offset += currentSlice.multiplier / combinedMultipliers;
            }
            return offset;
        }

        /// <summary>
        /// Calculate the perlin noise offset based on an index and an array.
        /// </summary>
        /// <param name="i">The index you are currently on.</param>
        /// <param name="seasons">The BiomeData you are looking through.</param>
        /// <returns>The perlin noise offset.</returns>
        static float Offset(int i, BiomeData biomeData)
        {
            float combinedMultipliers = CombinedMultipliers(biomeData);
            float offset = 0;
            for (int k = 0; k < i; k++)
            {
                BiomeData.BiomeTileData currentBiomeTileData = biomeData.biomeTileData[k];
                offset += currentBiomeTileData.multiplier / combinedMultipliers;
            }
            return offset;
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
        /// Used for calculating the total multipliers of all BiomeTileData in a given BiomeData.
        /// </summary>
        /// <param name="biomes">Biome to calculate the combined multipliers from</param>
        /// <returns>The combined multiplier value from a given BiomeData</returns>
        public static float CombinedMultipliers(BiomeData biome)
        {
            float result = 0;
            for (int i = 0; i < biome.biomeTileData.Length; i++)
            {
                BiomeData.BiomeTileData biomeTileData = biome.biomeTileData[i];
                result += biomeTileData.multiplier;
            }
            return result;
        }

        /// <summary>
        /// Used for calculating the total multipliers of all SliceData given.
        /// </summary>
        /// <param name="biomes">SliceData array to calculate the combined multipliers from</param>
        /// <returns>The combined multiplier value from all SliceData given</returns>
        public static float CombinedMultipliers(SeasonalData.HeightSlice.SliceData[] biomes)
        {
            float total = 0;
            foreach (SeasonalData.HeightSlice.SliceData data in biomes)
            {
                total += data.multiplier;
            }
            return total;
        }

        /// <summary>
        /// Used for calculating the total multipliers of all SeasonalData given.
        /// </summary>
        /// <param name="biomes">SeasonalData array to calculate the combined multipliers from</param>
        /// <returns>The combined multiplier value from all SeasonalData given</returns>
        public static float CombinedMultipliers(SeasonalData[] seasonals)
        {
            float total = 0;
            foreach (SeasonalData data in seasonals)
            {
                total += data.multiplier;
            }
            return total;
        }

        /// <summary>
        /// Used for calculating the total multipliers of all HeightSlice given.
        /// </summary>
        /// <param name="biomes">HeightSlice array to calculate the combined multipliers from</param>
        /// <returns>The combined multiplier value from all HeightSlice given</returns>
        public static float CombinedMultipliers(SeasonalData.HeightSlice[] slices)
        {
            float total = 0;
            foreach (var data in slices)
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
        /// <param name="tile">The tile of which you want the threshold modifier.</param>
        /// <param name="biomes">The biome array to look in.</param>
        /// <returns>The threshold modifer of this tile.</returns>
        public static float ThresholdModifier(Tile tile, BiomeData[] biomes)
        {
            foreach (BiomeData biome in biomes)
            {
                foreach (BiomeData.BiomeTileData data in biome.biomeTileData)
                {
                    if (data.tile == tile.GetTileData())
                    {
                        return data.multiplier;
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

        /// <summary>
        /// Returns the heightslices calculated from a grid position.
        /// </summary>
        /// <param name="noiseCollection">The noise collection you wish to use for your calculation.</param>
        /// <param name="generator">The generator you wish to base the grid position on.</param>
        /// <param name="x">The grid X position.</param>
        /// <param name="y">The grid Y position.</param>
        /// <returns>An array of heightslices.</returns>
        public static SeasonalData.HeightSlice[] SlicesFromPerlin(NoiseCollection noiseCollection, PerlinGenerator generator, int x, int y)
        {
            float[,] seasonNoise = noiseCollection.SeasonalMap();
            float[,] heightNoise = noiseCollection.HeightMap();
            float[,] biomeNoise = noiseCollection.BiomeMap();

            int result = 0;

            // Seasons
            float seasonalOffset = 0;
            float seasonalMultipliers = CombinedMultipliers(generator.seasons);
            for (int s = 0; s < generator.seasons.Length; s++)
            {
                SeasonalData season = generator.seasons[s];
                float seasonMax = seasonalOffset + (season.multiplier / seasonalMultipliers);
                float seasonMin = seasonalOffset;

                // Height slices in season
                float heightOffset = 0;
                float heightMultipliers = CombinedMultipliers(season.heightSlices);
                for (int h = 0; h < season.heightSlices.Length; h++)
                {
                    SeasonalData.HeightSlice slice = season.heightSlices[h];
                    float heightMax = heightOffset + (slice.multiplier / heightMultipliers);
                    float heightMin = heightOffset;

                    // Biomes in slice
                    float biomeMultipliers = CombinedMultipliers(slice.sliceBiomes);
                    float biomeOffset = 0;
                    for (int b = 0; b < slice.sliceBiomes.Length; b++)
                    {
                        SeasonalData.HeightSlice.SliceData biome = slice.sliceBiomes[b];
                        float biomeMax = biomeOffset + (biome.multiplier / biomeMultipliers);
                        float biomeMin = biomeOffset;

                        // cap off noise in order to avoid errors.
                        float perlinSeason = PerlinNoise.CapNoise(seasonNoise[x, y]);
                        float perlinHeight = PerlinNoise.CapNoise(heightNoise[x, y]);
                        float perlinBiome = PerlinNoise.CapNoise(biomeNoise[x, y]);

                        // Check here
                        if (perlinSeason >= seasonMin && perlinSeason <= seasonMax)
                        {
                            if (perlinHeight >= heightMin && perlinHeight <= heightMax)
                            {
                                if (perlinBiome >= biomeMin && perlinBiome <= biomeMax)
                                {
                                    result = s;
                                }
                            }
                        }
                        // Increase offsets
                        biomeOffset += biome.multiplier / biomeMultipliers;
                    }
                    heightOffset += slice.multiplier / heightMultipliers;
                }
                seasonalOffset += season.multiplier / seasonalMultipliers;
            }

            return generator.seasons[result].heightSlices;
        }

        /// <summary>
        /// Returns the slicedata calculated from a grid position.
        /// </summary>
        /// <param name="noiseCollection">The noise collection you wish to use for your calculation.</param>
        /// <param name="generator">The generator you wish to base the grid position on.</param>
        /// <param name="x">The grid X position.</param>
        /// <param name="y">The grid Y position.</param>
        /// <returns>An array : {SeasononalData index , HeightSlice index , SliceData index}.</returns>
        public static int[] SliceFromPerlin(NoiseCollection noiseCollection, PerlinGenerator generator, int x, int y)
        {
            float[,] seasonNoise = noiseCollection.SeasonalMap();
            float[,] heightNoise = noiseCollection.HeightMap();
            float[,] biomeNoise = noiseCollection.BiomeMap();

            // Seasons
            float seasonalOffset = 0;
            float seasonalMultipliers = CombinedMultipliers(generator.seasons);
            for (int s = 0; s < generator.seasons.Length; s++)
            {
                SeasonalData season = generator.seasons[s];
                float seasonMax = seasonalOffset + (season.multiplier / seasonalMultipliers);
                float seasonMin = seasonalOffset;

                // Height slices in season
                float heightOffset = 0;
                float heightMultipliers = CombinedMultipliers(season.heightSlices);
                for (int h = 0; h < season.heightSlices.Length; h++)
                {
                    SeasonalData.HeightSlice slice = season.heightSlices[h];
                    float heightMax = heightOffset + (slice.multiplier / heightMultipliers);
                    float heightMin = heightOffset;

                    // Biomes in slice
                    float biomeMultipliers = CombinedMultipliers(slice.sliceBiomes);
                    float biomeOffset = 0;
                    for (int b = 0; b < slice.sliceBiomes.Length; b++)
                    {
                        SeasonalData.HeightSlice.SliceData biome = slice.sliceBiomes[b];
                        float biomeMax = biomeOffset + (biome.multiplier / biomeMultipliers);
                        float biomeMin = biomeOffset;

                        // cap off noise in order to avoid errors.
                        float perlinSeason = PerlinNoise.CapNoise( seasonNoise[x, y] ) ;
                        float perlinHeight = PerlinNoise.CapNoise( heightNoise[x, y] ) ;
                        float perlinBiome = PerlinNoise.CapNoise( biomeNoise[x, y] );

                        // Check here
                        if (perlinSeason >= seasonMin && perlinSeason <= seasonMax)
                        {
                            if (perlinHeight >= heightMin && perlinHeight <= heightMax)
                            {
                                if (perlinBiome >= biomeMin && perlinBiome <= biomeMax)
                                {
                                    return new int[3] { s , h , b };
                                }
                            }
                        }
                        // Increase offsets
                        biomeOffset += biome.multiplier / biomeMultipliers;
                    }
                    heightOffset += slice.multiplier / heightMultipliers;
                }
                seasonalOffset += season.multiplier / seasonalMultipliers;
            }

            return null;
        }
    }
}