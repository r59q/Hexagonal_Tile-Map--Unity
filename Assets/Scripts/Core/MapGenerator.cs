using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// The MapGenerator is a base for generator scripts.
    /// </summary>
    public abstract class MapGenerator : MonoBehaviour
    {
        /// <summary>
        ///  TileMapData is a data container for data related to the map generation of hexagonal tiles.
        /// </summary>
        [Serializable]
        public class TileMapData
        {
            [Tooltip("The size of the map you want. In integers only!")]
            public Vector2 size;
            [Tooltip("The width of the hexagonal tile's sides on the prefabs you are going to use.")]
            public float sideWidth;
            [Tooltip("The gap you want between the tiles.")]
            public float gap;
            [Tooltip("The height offset you want the tiles to spawn from. Tiles spawn based on the MapGenerators position in world space.")]
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