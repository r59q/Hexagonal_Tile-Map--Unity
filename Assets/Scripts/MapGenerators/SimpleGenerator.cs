using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class SimpleGenerator : MapGenerator
    {
        public TileData baseTileData;

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

        /// <summary>
        /// Obsolete : Use Tile.Instantiate() instead.
        /// </summary>
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
            tileMap[i, k] = new Tile(baseTileData, GetPos(index), heightOffset, this, new Vector2(i, k));
        }

        protected bool IsGenerated(Tile[,] map)
        {
            // check
            bool check = true;
            for (int i = 0; i < mapSize.x; i++)
            {
                for (int k = 0; k < mapSize.y; k++)
                {
                    if (map[i, k].GetTileData() == baseTileData)
                    {
                        check = false;
                        break;
                    }
                }
                if (check == false)
                {
                    break;
                }
            }

            // return
            if (check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}