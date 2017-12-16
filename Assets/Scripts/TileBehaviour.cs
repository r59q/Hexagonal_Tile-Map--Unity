using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    public abstract class TileBehaviour : MonoBehaviour
    {

        TileData tile = null;

        public void Initialize(TileData tileData)
        {
            if (tile != null)
            {
                print("<color=olive>Warning! Could not initialize the tile.</color>\n"+ "It seems like you have initialized it more than once");
            } else { 
                tile = tileData;
                OnInitialize();
            }
        }

        private void Update()
        {
            if (tile != null)
            {
                OnUpdate();
            }
        }

        protected abstract void OnInitialize();
        protected abstract void OnUpdate();

    }
}