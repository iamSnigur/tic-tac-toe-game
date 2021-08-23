using UnityEngine;

public class Board : MonoBehaviour
{
    private BoardCell[] _cells;

    private float _cellSize;

    private Vector2Int _size;    

    public void Initialize(Vector2Int size, float cellSize)
    {
        _cellSize = cellSize;
        _size = size;
        _cells = new BoardCell[size.x * size.y];
    }

    public BoardCell GetCell(Ray ray)
    {
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            var x = (int)((hit.point.x / _cellSize + _size.x * 0.5f));
            var y = (int)((hit.point.z / _cellSize + _size.y * 0.5f));

            if (x >= 0 && x < _size.x && y >= 0 && y < _size.y)
            {
                Debug.Log($"x: {x}, y: {y}");
                return _cells[x + y * _size.x];
            }
        }

        return null;
    }

    public void ToggleCell(BoardCell cell)
    {
        if(cell.State == CellState.EMPTY)
        {
            
        }
    }
}
