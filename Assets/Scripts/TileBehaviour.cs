using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public abstract class TileBehaviour : MonoBehaviour
    {
        protected TileData tileData = null;
        protected Tile tile;

        public void Initialize(TileData newTileData, Tile tileContainer)
        {
            if (tile != null)
            {
                print("<color=olive>Warning! Could not initialize the tile.</color>\n"+ "It seems like you have initialized it more than once");
            } 

            tileData = newTileData;
            tile = tileContainer;
        }

    }
}