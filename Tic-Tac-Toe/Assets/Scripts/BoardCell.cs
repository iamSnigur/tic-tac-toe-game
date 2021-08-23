using System;

public enum CellState
{
    EMPTY,
    X,
    O
}

public class BoardCell
{
    private CellState _state;

    public CellState State
    {
        get => _state;

        set
        {
            
        }
    }

    public BoardCell()
    {
        _state = CellState.EMPTY;
    }
}
