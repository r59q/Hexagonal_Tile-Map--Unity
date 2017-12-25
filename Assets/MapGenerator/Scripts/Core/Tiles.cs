using UnityEngine;

namespace HexaMap
{
    /// <summary>
    /// Contains functionality for handling tiles
    /// </summary>
    public static class Tiles
    {
        /// <summary>
        /// Returns a Tile which will be an exact copy of the given Tile.
        /// </summary>
        /// <param name="tileToCopy">The Tile you wish to copy</param>
        /// <returns>The Tile you copied</returns>
        public static Tile CopyTile(Tile tileToCopy)
        {
            return new Tile(
                tileToCopy.GetTileData(),
                tileToCopy.Generator().tileMap.GetWorldPos(tileToCopy.Index()),
                tileToCopy.Height(),
                tileToCopy.Generator(),
                tileToCopy.Index());

        }

    }
}