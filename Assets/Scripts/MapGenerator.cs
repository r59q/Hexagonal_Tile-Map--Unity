using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace HexaMap
{
    public abstract class MapGenerator : MonoBehaviour
    {

        public TileMap tileMap = new TileMap();

        public abstract void Build();


    }
}