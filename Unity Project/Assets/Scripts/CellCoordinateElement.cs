using UnityEngine;

public class CellCoordinateElement : MonoBehaviour
{
    public CellCoordinates cellCoordinates;

    public virtual void MoveToCell(CellCoordinates toCellCoordinates)
    {
        transform.position = GameManagerScript.Instance.tileDatas[toCellCoordinates.Row, toCellCoordinates.Column].worldPos;
        cellCoordinates.Row = toCellCoordinates.Row;
        cellCoordinates.Column = toCellCoordinates.Column;
    }
}