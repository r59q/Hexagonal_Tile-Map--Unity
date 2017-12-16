using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public class MapHandler : MonoBehaviour
    {

        public TileData tileData;

        public Vector2 mapSize;
        public float sideWidth;
        public float gap = 0;

        Tile[,] tileMap;

        float tileWidth;
        float tileHeight;

        // the width multiplier
        float constant = 0.866f;

        private void Start()
        {
            // sets variables like tilewidth and tileheight
            Initialize();
        }

        public void BuildMap()
        {
            // Loop through all tiles
            for (int i = 0; i < mapSize.x; i++)
            {
                for (int k = 0; k < mapSize.y; k++)
                { 
                    // make a new tile position
                    Vector2 pos = new Vector2(i, k);

                    // create the tile. Upon creation it will instantiate itself
                    tileMap[i, k] = new Tile(tileData, GetPos(pos));
                }
            }
        }

        public Tile Tile(Vector2 position)
        {
            return tileMap[(int)position.x,(int)position.y];
        }
        
        public Tile Tile(int x, int y)
        {
            return tileMap[x, y];
        }

        Vector3 GetPos(Vector2 tile)
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

            Vector3 result = new Vector3(x,
                originalPos.y,
                z);

            return result;
        }

        void Initialize()
        {
            tileMap = new Tile[(int)mapSize.x,(int)mapSize.y];


            tileHeight = 2 * sideWidth;
            tileWidth = 2 * (sideWidth * constant);
        }

    }
}