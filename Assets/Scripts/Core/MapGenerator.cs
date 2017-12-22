using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace HexaMap
{
    public abstract class MapGenerator : MonoBehaviour
    {

        [Serializable]
        public class TileMapData
        {
            public Vector2 size;
            public float sideWidth;
            public float gap;
            public float heightOffset;
        }

        public TileMapData tileMapData = new TileMapData();

        public TileMap tileMap;

        private void Awake()
        {
            tileMap = new TileMap((int)tileMapData.size.x, (int)tileMapData.size.y,tileMapData.sideWidth,tileMapData.heightOffset,tileMapData.gap);
        }

        public abstract void Build();


    }
}