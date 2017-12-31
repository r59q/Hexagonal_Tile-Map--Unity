using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HexaMap
{
    /// <summary>
    /// This is the seasonal data container class.
    /// </summary>
    [CreateAssetMenu(menuName = "New seasonal data asset")]
    [System.Serializable]
    public class SeasonalData : ScriptableObject
    {
        [System.Serializable]
        public class HeightSlice
        {
            [System.Serializable]
            public class SliceData
            {
                public BiomeData biomeData;
                /// <summary>
                /// A variable, which is used to tweak how frequently this biome should appear. Lower means less frequent. Should not be below 1.
                /// </summary>
                public float multiplier = 1;
            }
            public SliceData[] sliceBiomes;

            public float multiplier = 1;
        }
        

        public HeightSlice[] heightSlices;

        public float multiplier = 1;

    }
}