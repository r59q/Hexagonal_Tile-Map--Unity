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
            /// <summary>
            /// The size of the map in a Vector2.
            /// </summary>
            [Tooltip("The size of the map you want. In integers only!")]
            public Vector2 size;

            /// <summary>
            /// The width of the hexagonal tile's sides on the prefabs you are going to use.
            /// </summary>
            [Tooltip("The width of the hexagonal tile's sides on the prefabs you are going to use.")]
            public float sideWidth;

            /// <summary>
            /// The gap you want between the tiles.
            /// </summary>
            [Tooltip("The gap you want between the tiles.")]
            public float gap;

            /// <summary>
            /// The height offset you want the tiles to spawn from.
            /// </summary>
            [Tooltip("The height offset you want the tiles to spawn from. Tiles spawn based on the MapGenerators position in world space.")]
            public float heightOffset;
        }

        /// <summary>
        /// The data related to map generation.
        /// </summary>
        public TileMapData tileMapData = new TileMapData();

        /// <summary>
        /// The TileMap object related to this generator.
        /// </summary>
        public TileMap tileMap;

        private void Awake()
        {
            tileMap = new TileMap((int)tileMapData.size.x, (int)tileMapData.size.y,tileMapData.sideWidth,tileMapData.heightOffset,tileMapData.gap);
        }

        /// <summary>
        /// Used by other scripts in order to generate a map. 
        /// </summary>
        public abstract void Build();
    }
}