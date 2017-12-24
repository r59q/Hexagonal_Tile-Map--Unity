using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    public class SimpleGenerator : MapGenerator
    {
        public override void Build()
        {
            // Loop through all tiles
            for (int i = 0; i < tileMap.size.x; i++)
            {
                for (int k = 0; k < tileMap.size.y; k++)
                {
                    Spawn(i, k, null);
                }
            }
            OnInitialized();
        }
    
        protected void InstantiateAll()
        {
            for (int i = 0; i < tileMap.size.x; i++)
            {
                for (int k = 0; k < tileMap.size.y; k++)
                {
                    tileMap.Map()[i, k].Instantiate();
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

        // For making Tile objects, not instantiating prefabs
        protected virtual void Spawn(int i, int k, TileData tileData)
        {
            // make a new tile index
            Vector2 index = new Vector2(i, k);

            // create the tile. Upon creation it will instantiate itself
            tileMap.Map()[i, k] = new Tile(tileData, tileMap.GetWorldPos(index), tileMap.HeightOffset(), this, new Vector2(i, k));
        }

        protected bool IsGenerated(Tile[,] map)
        {
            // check
            bool check = true;
            for (int i = 0; i < tileMap.size.x; i++)
            {
                for (int k = 0; k < tileMap.size.y; k++)
                {
                    if (map[i, k].GetTileData() == null)
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