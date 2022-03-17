using UnityEngine;
using UnityEngine.Tilemaps;

namespace UsefulScripts.UnityScripts
{
    public static class TilemapS
    {
        public static void SetTile(this Tilemap tilemap, Vector2Int pos, TileBase tileBase)
        {
            tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), tileBase);
        }
    }
}