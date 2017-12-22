using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    [CreateAssetMenu (menuName = "New TileData")]
    public class TileData : ScriptableObject
    {
        public GameObject prefab;
    }
}