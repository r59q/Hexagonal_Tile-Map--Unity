using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public class TileMap
    {

        public Vector2 size;

        protected Tile[,] map;

        protected int tileCount;

        float tileWidth;
        float tileHeight;

        float heightOffset;
        float sideWidth;

        float gap;

        // the width multiplier
        float constant = 0.866f;

        public TileMap(int x , int y, float tileSideWidth, float originHeightOffset ,float tileGap)
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


        // For grabbing a certain tile based on the tile's position
        public Tile Tile(Vector2 position)
        {
            return map[(int)position.x, (int)position.y];
        }
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

        // For grabbing the world space coordinates
        public Vector3 GetWorldPos(Vector2 tile)
        {
            Vector3 originalPos = GameManager.instance.transform.position;

            float offset;

            if (tile.y % 2 == 0)
            {
                offset = tileWidth / 2;
            }
            else
            {
                offset = 0;
            }

            float x = (originalPos.x + (((tileWidth) + gap) * tile.x)) + offset;
            // we convert y to z, because we are goin on a horizontal plane
            float z = originalPos.z + ((((sideWidth / 2) * 3) + gap) * tile.y);

            Vector3 result = new Vector3(x, originalPos.y, z);

            return result;
        }

        public Vector3 GetWorldPos(Tile tile)
        {
            return GetWorldPos(tile.Index());
        }

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

            tileHeight = 2 * sideWidth;
            tileWidth = 2 * (sideWidth * constant);
        }

    }
}