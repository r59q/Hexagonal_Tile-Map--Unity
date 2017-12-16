using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class BiomeGenerator : SimpleGenerator {

        public BiomeData[] biomes;

        protected override void Spawn(int i, int k)
        {
            base.Spawn(i, k);
            Tile currentTile = tileMap[i, k];
        }

        TileData GetTileData()
        {
            return null;
        }

    }
}