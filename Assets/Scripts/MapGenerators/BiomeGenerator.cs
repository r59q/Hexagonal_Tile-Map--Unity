using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class BiomeGenerator : SimpleGenerator {

        public BiomeData[] biomes;


        // For when all tiles have been loaded
        protected override void OnInitialized()
        {
            int biomeTileCount = biomes.Length / tileCount;
            foreach (BiomeData data in biomes)
            {
                
            }

            for (int i = 0; i < mapSize.x; i++)
            {
                for (int k = 0; k < mapSize.y; k++)
                {
                    print (tileMap[i, k].Neighbours().Length);
                }
            }

            InstantiateAll();
        }

    }
}