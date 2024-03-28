using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class WorldController : MonoBehaviour
{
    [SerializeField] private Tilemap _ground;
    [SerializeField] private Tilemap _water;
    [SerializeField] private Tilemap _waterCoast;

    [SerializeField] private TileBase _waterTile;
    [SerializeField] private TileBase _coastWaterTile;
    [SerializeField] private TileBase _coastTile;

    private IReadOnlyDictionary<Vector2Int,CellType> _cells;

    private CellGenerator _cellGenerator;
    private WaterController _waterController;

    private Vector2Int _worldSize;
    private void Awake()
    {
        _cellGenerator = new CellGenerator(_ground);
        _cells = _cellGenerator.Generate();

        _waterController = new WaterController(_water, _waterTile, _cells);

        _worldSize = new Vector2Int((_ground.size.x / 16 + 4) * 16, (_ground.size.y / 16 + 4) * 16);

        GenerateWater();
        StartCoroutine(GenerateCoastRoutine());
    }
    private void GenerateWater()
    {
        var sizeX = _worldSize.x;
        var sizeY = _worldSize.y;

        for (var x = -sizeX / 2; x < sizeX / 2; x += 16)
        {
            for (var y = -sizeY / 2; y < sizeY / 2; y += 16)
            {
                _waterController.Load(new Vector2Int(x, y));
            }
        }
        ScreenFader.instance.Unfade();//super bad practice
    }
    private IEnumerator GenerateCoastRoutine()
    {
        foreach(var cell in _cells.Keys) { 
            var cellType = _cells[cell];
            if(cellType == CellType.Coast)
            {
                _ground.SetTile(new Vector3Int(cell.x, cell.y, 0), _coastTile);
                _waterCoast.SetTile(new Vector3Int(cell.x,cell.y,0), _coastWaterTile);
                yield return null;
            }
        }
    }
    private IEnumerator GenerateWaterRotine()
    {
        var sizeX = _worldSize.x;
        var sizeY = _worldSize.y;

        for (var x = -sizeX / 2; x < sizeX / 2; x+=16)
        {
            for (var y = -sizeY / 2; y < sizeY / 2; y+=16)
            {
                _waterController.Load(new Vector2Int(x, y));
                yield return null;
                yield return null;
            }
        }
        
    }
}