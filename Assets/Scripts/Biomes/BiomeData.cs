using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    [CreateAssetMenu (menuName = "New biome data asset")]
    public class BiomeData : ScriptableObject
    {
        [System.Serializable]
        public class BiomeTileData
        {
            public TileData tile;
            public float threshold;
        }

        public BiomeTileData[] biomeTileData;

        public float multiplier = 1;

    }
}