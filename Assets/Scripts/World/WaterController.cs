using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterController
{
    public Color Color;
    private Tilemap _water;
    private TileBase _tile;
    private IReadOnlyDictionary<Vector2Int,CellType> _cells;

    public WaterController(Tilemap water, TileBase tile, IReadOnlyDictionary<Vector2Int, CellType> cells)
    {
        _water = water;
        _tile = tile;
        _cells = cells; 
    }

    public void Load(Vector2Int position)
    {
        SetTile(position, _tile);
    }
    public void Unload(Vector2Int position)
    {
        SetTile(position, null);
    }
    private void SetTile(Vector2Int position, TileBase tile)
    {
        var size = 16;
        for (var x = position.x; x < position.x + size; x++)
        {
            for (var y = position.y; y < position.y + size; y++)
            {
                if (_cells.TryGetValue(new Vector2Int(x, y), out var cellType) && cellType != CellType.Ground) {
                     _water.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }
}
