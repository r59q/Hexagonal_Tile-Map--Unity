using UnityEngine;

namespace HexaMap
{
    public static class Tiles
    {
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