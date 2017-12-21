using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class BiomeGenerator : SimpleGenerator {

        public BiomeData[] biomes;


        // For when all tiles have been loaded
        protected override void OnInitialized()
        {

            tileMap.Map(GenerateTileMap(tileMap.Map(), (int)tileMap.size.x, (int)tileMap.size.y));

            for (int i = 0; i < tileMap.size.x; i++)
            {
                for (int k = 0; k < tileMap.size.y; k++)
                {
                    // print (tileMap[i, k].Neighbours().Length);
                }
            }

            InstantiateAll();
        }

        Tile[,] GenerateTileMap(Tile[,] baseMap, int xSize, int ySize)
        {
            // Copy map array
            Tile[,] result = new Tile[xSize, ySize];

            for (int i = 0; i < xSize; i++)
            {
                for (int k = 0; k < ySize; k++)
                {
                    result[i, k] = Tiles.CopyTile( baseMap[i, k] );
                }
            }

            //Randomize
            float maxRoll = 10000;
            float biomeThreshold = maxRoll / biomes.Length;

            while(!IsGenerated(result))
            {
                foreach (BiomeData data in biomes)
                {
                    if (UnityEngine.Random.Range(0f, maxRoll * data.multiplier) >= biomeThreshold )
                    {
                        print("seeding");
                        result = SeedBiome(result, data, xSize, ySize);
                        //print(result[4, 4].GetTileData());
                    }
                }
            }
            
            return result;
        }

        Tile[,] SeedBiome(Tile[,] map,BiomeData data,int xSize, int ySize)
        {
            // pre work
            Tile[,] result = map;

            // Select random tile pos
            Vector2 originGridPos = new Vector2(
                UnityEngine.Random.Range(0, xSize),
                UnityEngine.Random.Range(0, ySize));
            // Convert to tile object and find neighbours
            //Tile lookingAt = ;
            Tile[] neighbours = result[(int)originGridPos.x, (int)originGridPos.y].Neighbours();
            List<Vector2> neighboursIndexes = new List<Vector2>();
            List<Vector2> neighboursNeighboursIndexes = new List<Vector2>();
            List<Vector2> totalNeighboursIndexes = new List<Vector2>();
            foreach (Tile neighbour in neighbours)
            {
                Vector2 neighbourIndex = neighbour.Index();
                neighboursIndexes.Add(neighbourIndex);
                if(!totalNeighboursIndexes.Contains(neighbourIndex))
                {
                    totalNeighboursIndexes.Add(neighbourIndex);
                }
            }
            foreach (Tile neighbour in neighbours)
            {
                foreach (Tile neighboursNeighbour in neighbour.Neighbours())
                {
                    if (!neighboursIndexes.Contains(neighboursNeighbour.Index()))
                    {
                        neighboursNeighboursIndexes.Add(neighboursNeighbour.Index());
                    }
                    if (!totalNeighboursIndexes.Contains(neighboursNeighbour.Index()))
                    {
                        totalNeighboursIndexes.Add(neighboursNeighbour.Index());
                    }
                }
            }

            // will be removed, in use for testing purposes
            if (result[(int)originGridPos.x, (int)originGridPos.y].GetTileData() == baseTileData) {
                 int random = UnityEngine.Random.Range(0, data.tiles.Length);

                result[(int)originGridPos.x, (int)originGridPos.y].SetTileData(data.tiles[random]);

            }
            //work

            List<BiomeData> nearbyBiomesList = new List<BiomeData>();
            int[] biomeCounter = new int[biomes.Length];

            foreach (Vector2 index in totalNeighboursIndexes)
            {
                Tile neighbour = result[(int)index.x, (int)index.y];

                foreach (BiomeData biome in biomes)
                {
                    // Get biomeIndex
                    int biomeIndex = 0;
                    for (int i = 0; i < biomes.Length; i++)
                    {
                        if (biome == biomes[i])
                        {
                            biomeIndex = i;
                        }
                    }

                    foreach (TileData tileData in biome.tiles)
                    {
                        if (neighbour.GetTileData() == tileData)
                        {
                            //print("tileDataFound");
                            if (!nearbyBiomesList.Contains(biome)) {
                                nearbyBiomesList.Add(biome);
                            }
                            biomeCounter[biomeIndex] = biomeCounter[biomeIndex]+1;
                            print(biomeIndex +" - " +biomeCounter[biomeIndex]);
                        }
                    }
                }

                for (int i = 0; i < biomeCounter.Length; i++)
                {
                    //print(i +" : " + biomeCounter[i]);
                }

            }

            return result;
        }

    }
}