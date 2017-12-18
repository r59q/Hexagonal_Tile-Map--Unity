using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    [CreateAssetMenu (menuName = "New biome data asset")]
    public class BiomeData : ScriptableObject
    {
        public TileData[] tiles;
        public float multiplier = 1;

    }
}