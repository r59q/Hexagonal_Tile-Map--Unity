using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace HexaMap
{
    public abstract class MapGenerator : MonoBehaviour
    {
        public Vector2 mapSize = new Vector2(50,50);
        public float heightOffset;

        [Space(8)]

        public float sideWidth = 1;
        public float gap = 0;

        protected Tile[,] tileMap;

        float tileWidth;
        float tileHeight;

        // the width multiplier
        float constant = 0.866f;

        private void Start()
        {
            // sets variables like tilewidth and tileheight
            Initialize();
        }

        public abstract void Build();

        // For grabbing a certain tile based on the tile's position
        public Tile Tile(Vector2 position)
        {
            return tileMap[(int)position.x,(int)position.y];
        }
        public Tile Tile(int x, int y)
        {
            return tileMap[x, y];
        }

        // For grabbing the world space coordinates
        public Vector3 GetPos(Vector2 tile)
        {
            Vector3 originalPos = transform.position;

            float offset;

            if (tile.y % 2 == 0)
            {
                offset = tileWidth / 2;
            }
            else
            {
                offset = 0;
            }

            float x = (originalPos.x + (((tileWidth)+gap) * tile.x)) + offset;
            // we convert y to z, because we are goin on a horizontal plane
            float z = originalPos.z + ((((sideWidth / 2) * 3)+gap) * tile.y);

            Vector3 result = new Vector3(x , originalPos.y , z);

            return result;
        }

        /// <summary>
        /// Not good performance. Use GetPos(Vector2 index) instead
        /// </summary>
        public Vector3 GetPos(Tile tile)
        {
            for (int i = 0; i < mapSize.x; i++)
            {
                for (int k = 0; k < mapSize.y; k++)
                {
                    if (tileMap[i,k] == tile)
                    {
                        return GetPos(new Vector2(i, k));
                    }
                }
            }
            return Vector3.zero;
        }


        // For local use
        void Initialize()
        {
            tileMap = new Tile[(int)mapSize.x,(int)mapSize.y];

            tileHeight = 2 * sideWidth;
            tileWidth = 2 * (sideWidth * constant);
        }

    }
}