using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public class SimpleGenerator : MapGenerator
    {
        public TileData tileData;

        public override void Build()
        {
            // Loop through all tiles
            for (int i = 0; i < mapSize.x; i++)
            {
                for (int k = 0; k < mapSize.y; k++)
                {
                    // make a new tile position
                    Vector2 pos = new Vector2(i, k);

                    // create the tile. Upon creation it will instantiate itself
                    tileMap[i, k] = new Tile(tileData , GetPos(pos) , heightOffset , this,new Vector2(i,k));
                }
            }

        }
    }
}