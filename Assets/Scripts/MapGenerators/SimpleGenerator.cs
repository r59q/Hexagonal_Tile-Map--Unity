using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
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
                    Spawn(i, k);
                }
            }
            OnInitialized();
        }
    
        protected void InstantiateAll()
        {
            for (int i = 0; i < mapSize.x; i++)
            {
                for (int k = 0; k < mapSize.y; k++)
                {
                    tileMap[i, k].Instantiate();
                }
            }
        }

        protected void InstantiateTile(Tile tile)
        {
            tile.Instantiate();
        }

        protected virtual void OnInitialized()
        {

        }

        protected virtual void Spawn(int i, int k)
        {
            // make a new tile index
            Vector2 index = new Vector2(i, k);

            // create the tile. Upon creation it will instantiate itself
            tileMap[i, k] = new Tile(tileData, GetPos(index), heightOffset, this, new Vector2(i, k));
        }
    }
}