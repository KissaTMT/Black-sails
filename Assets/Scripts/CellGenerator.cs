using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CellType : byte
{
    Water,
    Ground,
    Coast
}
public class CellGenerator
{
    private Dictionary<Vector2Int, CellType> _cells;
    private Tilemap _mask;

    public CellGenerator(Tilemap tilemap)
    {
        _mask = tilemap;
        _cells = new Dictionary<Vector2Int, CellType>();
    }

    public IReadOnlyDictionary<Vector2Int, CellType> Generate()
    {
        var size = new Vector2Int((_mask.size.x / 16 + 4) * 16, (_mask.size.y / 16 + 4) * 16);
        var sizeX = size.x;
        var sizeY = size.y;
        var middleX = sizeX / 2;
        var middleY = sizeY / 2;

        for (var x = -middleX; x < middleX; x++)
        {
            for (var y = -middleY; y < middleY; y++)
            {
                var tile = _mask.GetTile(new Vector3Int(x, y, 0));
                var position = new Vector2Int(x, y);
                if (_cells.ContainsKey(position)) continue;
                if (tile)
                {
                    _cells.Add(position, CellType.Ground);
                    var positions = GetFreePositions(position);
                    for (var i = 0; i < positions.Count; i++)
                    {
                        if (_cells.ContainsKey(positions[i])) _cells[positions[i]] = CellType.Coast;
                        else _cells.Add(positions[i], CellType.Coast);
                    }

                }
                else _cells.Add(position, CellType.Water);
            }
        }
        return _cells;
    }
    private List<Vector2Int> GetFreePositions(Vector2Int position)
    {
        var neighbors = new List<Vector2Int>();
        var moves = new int[2, 8]
        {
            {-1, 1, 0, 0, -1, -1, 1, 1},
            {0, 0, -1, 1, -1, 1, -1, 1}
        };
        for (var i = 0; i < 8; i++)
        {
            var offset = new Vector2Int(position.x + moves[0, i], position.y + moves[1, i]);
            if (_mask.GetTile(new Vector3Int(offset.x, offset.y, 0)) == null) neighbors.Add(offset);
        }

        return neighbors;
    }
}