using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public event Action<string> OnGameFinished;
    public event Action OnGameReseted;

    [SerializeField] private GameBoard _board;

    [SerializeField] private Camera _camera;

    private CellState _currentPlayer;

    private bool _isPlayerSelected;
    private bool _isGameOver;

    private AI _ai;

    private Vector2 _touch => _camera.ScreenToWorldPoint(Input.mousePosition);

    private Vector2Int _boardSize;   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isPlayerSelected && !_isGameOver)
        {
            if (HandleTouch())
            {
                if (_board.CheckState().HasEnded())
                {
                    _isGameOver = true;
                    OnGameFinished?.Invoke($"{_board.CheckState()}");
                    return;
                }

                (int, int) move = _ai.DoBestMove(_currentPlayer.SwitchPlayer(), _currentPlayer.SwitchPlayer().WhoShouldWin());
                _board.SetCellState(move.Item1, move.Item2, _currentPlayer.SwitchPlayer(), true);
            }
        }
    }

    private void InitializeGame()
    {
        _board.InitializeBoard(_boardSize);
        _ai = new AI(_board);
    }

    private bool HandleTouch()
    {
        var x = (int)(_touch.x + _board.Rows * 0.5f);
        var y = (int)(_touch.y + _board.Columns * 0.5f);

        return _board.SetCellState(y, x, _currentPlayer, true);
    }

    // Shitty code #1
    public void SetPlayerTokenX()
    {
        _currentPlayer = CellState.X;
        _isPlayerSelected = true;
    }

    // Shitty code #2
    public void SetPlayerTokenO()
    {
        _currentPlayer = CellState.O;
        _isPlayerSelected = true;
    }

    public void ResetGame()
    {
        _isGameOver = false;
        _board.ResetBoard();
        OnGameReseted?.Invoke();
    }

    public void SetDificulty(int rows)
    {
        _boardSize = new Vector2Int(rows, rows);
        InitializeGame();
    }
}