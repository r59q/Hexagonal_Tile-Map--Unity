using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HexaMap
{
    /// <summary>
    /// This is the biome data container class.
    /// </summary>
    [CreateAssetMenu (menuName = "New biome data asset")]
    public class BiomeData : ScriptableObject
    {
        /// <summary>
        /// This is the data container, which holds information about
        /// a tile in relation to a biome.
        /// </summary>
        [System.Serializable]
        public class BiomeTileData
        {
            /// <summary>
            ///  Holds the tile data, such as the tile prefab.
            /// </summary>
            public TileData tile;
            /// <summary>
            /// Threshold is a variable, which the biome uses to calculate 
            /// how to distribute different tiles in a zone.
            /// </summary>
            public float multiplier = 1;
        }

        /// <summary>
        /// The array of tiles to use in this biomes. 
        /// It contains data such as the tile data and the threshold.
        /// </summary>
        public BiomeTileData[] biomeTileData;

        /// <summary>
        /// A variable, which is used to tweak how frequently this biome should appear. 
        /// Lower means less frequent. Should not be below 1.
        /// </summary>
        public float multiplier = 1;

    }
}