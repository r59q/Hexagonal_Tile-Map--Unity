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

            tileMap = GenerateTileMap(tileMap, (int) mapSize.x, (int) mapSize.y);

            for (int i = 0; i < mapSize.x; i++)
            {
                for (int k = 0; k < mapSize.y; k++)
                {
                    // print (tileMap[i, k].Neighbours().Length);
                }
            }

            InstantiateAll();
        }

        Tile[,] GenerateTileMap(Tile[,] baseMap, int x, int y)
        {
            // Copy map array
            Tile[,] result = new Tile[x,y];

            for (int i = 0; i < x; i++)
            {
                for (int k = 0; k < y; k++)
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
                        result = SeedBiome(result, data, x, x);
                    }
                }
            }
            
            return result;
        }

        Tile[,] SeedBiome(Tile[,] map,BiomeData data,int x, int y)
        {
            Tile[,] result = (Tile[,])map.Clone();

            Vector2 originGridPos = new Vector2(
                UnityEngine.Random.Range(0, x),
                UnityEngine.Random.Range(0, y));

            if (result[(int)originGridPos.x, (int)originGridPos.y].GetTileData() == baseTileData) { 
                result[(int)originGridPos.x, (int)originGridPos.y].SetTileData(data.tiles[UnityEngine.Random.Range(0, data.tiles.Length)]);
            }
            return result;
        }

    }
}