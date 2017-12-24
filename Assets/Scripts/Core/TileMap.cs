using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// A TileMap is an object, which contains all the data related to the map and methods thereof. 
    /// Such as calculating index spaces into world space coordinates.
    /// </summary>
    public class TileMap
    {
        /// <summary>
        /// The size of the map in integers.
        /// </summary>
        public Vector2 size;

        /// <summary>
        /// The tiles of the map in an array.
        /// </summary>
        protected Tile[,] map;

        /// <summary>
        /// The total amount of tiles in the map.
        /// </summary>
        protected int tileCount;

        // Map generation settings.
        float tileWidth;
        float heightOffset;
        float sideWidth;
        float gap;

        // The width multiplier
        const float constant = 0.866f;

        /// <summary>
        /// Constructs a TileMap object. 
        /// It will create the array that contains all the tiles of the map, 
        /// and handle calculating the world space coordinates.
        /// </summary>
        /// <param name="x">The width of the map</param>
        /// <param name="y">The height of the map</param>
        /// <param name="tileSideWidth">The width of the sides of the hexagonal tiles, that you wish to use.</param>
        /// <param name="originHeightOffset">The origin height offset.</param>
        /// <param name="tileGap">The gap you wish between tiles.</param>
        public TileMap(
            int x, int y, 
            float tileSideWidth, 
            float originHeightOffset,
            float tileGap)
        {
            // set variables
            sideWidth = tileSideWidth;
            heightOffset = originHeightOffset;
            gap = tileGap;
            size.x = x; size.y = y;
            tileCount = (int)(size.x * size.y);

            // initialize
            Initialize();
        }


        /// <summary>
        /// Returns a certain tile based on the tile's grid index
        /// </summary>
        /// <param name="position">Index in the map grid</param>
        /// <returns>Tile based on index.</returns>
        public Tile Tile(Vector2 position)
        {
            return map[(int)position.x, (int)position.y];
        }

        /// <summary>
        /// Returns a certain tile based on the tile's grid index
        /// </summary>
        /// <param name="x">X index in the map grid</param>
        /// <param name="y">Y index in the map grid</param>
        /// <returns>Tile based on index.</returns>
        public Tile Tile(int x, int y)
        {

            if (x < 0 || x >= size.x)
            {
                return null;
            }

            if (y < 0 || y >= size.y)
            {
                return null;
            }
            return map[x, y];
        }

        /// <summary>
        /// Returns the world space coordinates based on a tile index.
        /// </summary>
        /// <param name="index">Index in the map grid</param>
        /// <returns>World space coodinates.</returns>
        public Vector3 GetWorldPos(Vector2 index)
        {
            Vector3 originalPos = GameManager.instance.transform.position;

            float offset;

            if (index.y % 2 == 0)
            {
                offset = tileWidth / 2;
            }
            else
            {
                offset = 0;
            }

            float x = ((tileWidth) + gap) * index.x;
            x += originalPos.x;
            x += offset;
            // we convert y to z, because we are goin on a horizontal plane
            float z = (((sideWidth / 2) * 3) + gap) * index.y;
            z += originalPos.z;

            Vector3 result = new Vector3(x, originalPos.y, z);

            return result;
        }

        /// <summary>
        /// Returns the world space coordinates of a given tile.
        /// </summary>
        /// <param name="tile">The tile you wish the coordinates of.</param>
        /// <returns>World space coordinates of the given tile</returns>
        public Vector3 GetWorldPos(Tile tile)
        {
            return GetWorldPos(tile.Index());
        }

        /// <summary>
        /// Returns an array of Tiles that are reprenting all the tiles in their appropriate indexes.
        /// </summary>
        /// <returns>An array of Tiles</returns>
        public Tile[,] Map() {
            return map;
        }

        public void Map(Tile[,] newMap)
        {
            map = newMap;
        }

        public float HeightOffset()
        {
            return heightOffset;
        }

        // For local use
        void Initialize()
        {
            map = new Tile[(int)size.x, (int)size.y];

            // tileHeight = 2 * sideWidth;
            tileWidth = 2 * (sideWidth * constant);
        }

    }
}