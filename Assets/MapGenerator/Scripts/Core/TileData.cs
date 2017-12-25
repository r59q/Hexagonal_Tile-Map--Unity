using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// The asset that contains the information about a tile prior to launch.
    /// </summary>
    [CreateAssetMenu (menuName = "New TileData")]
    public class TileData : ScriptableObject
    {
        [Tooltip("The tile prefab.")]
        public GameObject prefab;
    }
}