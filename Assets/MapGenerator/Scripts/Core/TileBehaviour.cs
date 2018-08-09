using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// A TileBehaviour is a MonoBehaviour script, which contains data about the tile.
    /// </summary>
    public abstract class TileBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The Tile object that is associated with this tile.
        /// </summary>
        protected Tile tile;
        
        /// <summary>
        /// Sets the internal Tile variable of this script.
        /// </summary>
        /// <param name="newTileData">It's TileData</param>
        /// <param name="tileContainer">It's Tile object</param>
        public void Initialize(Tile tileContainer)
        {
            if (tile != null)
            {
                print("<color=olive>Warning! Could not initialize the tile.</color>\n"+ "It seems like you have initialized it more than once");
            } 
            tile = tileContainer;
        }

        /// <summary>
        /// Used to peform actions before the map has been fully generated.
        /// You might want to change the position of a gameobject under the generation,
        /// because if you change it on the Start function and you do a NavMesh bake right
        /// after the level generation, the position would not be taken into consideration
        /// when generating the NavMesh.
        /// </summary>
        public virtual void BeforeStart() { }
    }
}