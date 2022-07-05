using UnityEngine;
[System.Serializable]
public class CellCoordinates
{
    [Range(0, GameManagerScript.boardRows -1), SerializeField]  int _row;
    [Range(0, GameManagerScript.boardColumns -1), SerializeField] private int _column;

    public int Row 
    {
        get { return _row; }
        set 
        {
            switch (value)
            {
                case < 0:
                    _row = 0;
                    break;
                case >= GameManagerScript.boardRows:
                    _row = GameManagerScript.boardRows - 1;
                    break;
                default:
                    _row = value;
                    break;
            }
        }
    }

    public int Column
    {
        get
        {
            return _column;
        }
        set
        {
            switch (value)
            {
                case < 0:
                    _column = 0;
                    break;
                case >= GameManagerScript.boardColumns:
                    _column = GameManagerScript.boardColumns -1;
                    break;
                default:
                    _column = value;
                    break;
            }
        }
    }

    public CellCoordinates(int row, int column)
    {
        this.Row = row;
        this.Column = column;
    }

}
