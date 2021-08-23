using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Board _board;

    [SerializeField] private Vector2Int _boardSize;

    [SerializeField] private float _boardCellSize;

    private Ray _touchRay => _camera.ScreenPointToRay(Input.mousePosition);

    private void Start()
    {
        _board.Initialize(_boardSize, _boardCellSize);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HandleInput();
        }
    }

    private void HandleInput()
    {
        var cell = _board.GetCell(_touchRay);
        
        if(cell != null)
        {
            _board.ToggleCell(cell);
        }
    }
}
