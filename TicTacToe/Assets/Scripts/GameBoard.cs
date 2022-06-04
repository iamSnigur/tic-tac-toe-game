using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    private CellState[,] _board;

    public int Rows { get; private set; }
    public int Columns { get; private set; }

    [SerializeField] private GameObject _xPrefab;
    [SerializeField] private GameObject _oPrefab;

    private List<GameObject> _tokens;

    public int MarkedCells { get; private set; }

    public void InitializeBoard(Vector2Int boardSize)
    {
        Rows = boardSize.y;
        Columns = boardSize.x;
        _tokens = new List<GameObject>();
        _board = new CellState[Rows, Columns];
        MarkedCells = 0;
    }

    public CellState GetCellState(int row, int column) => _board[row, column];

    public bool SetCellState(int row, int column, CellState state, bool setToken)
    {  
        if (column >= 0 && column < Columns && row >= 0 && row < Rows)
        {
            if(_board[row, column] != CellState.EMPTY && state != CellState.EMPTY)
            {
                return false;
            }

            if (setToken)
            {
                var offset = new Vector2((Columns - 1) * 0.5f, (Rows - 1) * 0.5f);

                var token = Instantiate((state == CellState.X) ? _xPrefab : _oPrefab);
                token.transform.SetParent(transform, false);
                token.transform.localPosition = new Vector2(column - offset.x, row - offset.y);

                _tokens.Add(token);
            }

            _board[row, column] = state;
            MarkedCells += (state == CellState.EMPTY) ? -1 : 1;
            return true;
        }

        return false;
    }    

    public GameResult CheckCells((int, int) cell1, (int, int) cell2, (int, int) cell3)
    {
        var c1 = GetCellState(cell1.Item1, cell1.Item2);
        var c2 = GetCellState(cell2.Item1, cell2.Item2);
        var c3 = GetCellState(cell3.Item1, cell3.Item2);

        if (!(c1 == c2 && c2 == c3) ||
                c1 == CellState.EMPTY ||
                c2 == CellState.EMPTY || 
                c3 == CellState.EMPTY)
        {
            return GameResult.UNKNOWN;
        }

        return (c2 == CellState.X) ? GameResult.XWINS : GameResult.OWINS;
    }

    public GameResult CheckState()
    {
        GameResult algorithmResult;

        for (int x = 0; x < Columns; x++)
        {
            algorithmResult = CheckCells((x, 0), (x, 1), (x, 2));
            if (algorithmResult.HasEnded()) return algorithmResult;
        }

        for (int y = 0; y < Rows; y++)
        {
            algorithmResult = CheckCells((0, y), (1, y), (2, y));
            if (algorithmResult.HasEnded()) return algorithmResult;
        }

        algorithmResult = CheckCells((0, 0), (1, 1), (2, 2));

        if (algorithmResult.HasEnded()) return algorithmResult;

        algorithmResult = CheckCells((0, 2), (1, 1), (2, 0));

        if (algorithmResult.HasEnded()) return algorithmResult;

        if (MarkedCells == Rows * Columns)
        {
            return GameResult.DRAW;
        }

        return GameResult.UNKNOWN;
    }

    public void ResetBoard()
    {
        _tokens.ForEach(t => Destroy(t));

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                _board[i, j] = CellState.EMPTY;
            }
        }
    }
}