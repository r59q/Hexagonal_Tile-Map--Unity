using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap.Generators
{
    /// <summary>
    /// A base class for other MapGenerators. Contains methods for generating Tiles.
    /// </summary>
    public abstract class SimpleGenerator : MapGenerator
    {
        /// <summary>
        /// Used for creating a set of Tiles within a TileMap.
        /// </summary>
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

        /// <summary>
        /// Instantiates all Tiles in the TileMap.
        /// </summary>
        public void InstantiateAll()
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
        /// Letting extensions know when the TileMap is ready for manipulation.
        /// </summary>
        protected abstract void OnInitialized();

        /// <summary>
        /// Creates a TileObject in the index that you specified, and returns it.
        /// </summary>
        /// <param name="x">X index in the TileMap grid</param>
        /// <param name="y">Y index in the TileMap grid</param>
        /// <param name="tileData">The TileData you wish associated with this Tile</param>
        /// <returns>The Tile object you have just created</returns>
        protected virtual Tile Spawn(int x, int y, TileData tileData)
        {
            // make a new tile index
            Vector2 index = new Vector2(x, y);

            // create the tile. Upon creation it will instantiate itself
            tileMap.Map()[x, y] = 
                new Tile(tileData, 
                tileMap.GetWorldPos(index), 
                tileMap.HeightOffset(), 
                this, 
                index);

            return tileMap.Map()[x, y];
        }

        /// <summary>
        /// Check if the entire map has been created and 
        /// has a prefab associated with it.
        /// </summary>
        /// <param name="map">The set of tiles you wish to know whether or not has been generated.</param>
        /// <returns>Whether or not it has been generated.</returns>
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