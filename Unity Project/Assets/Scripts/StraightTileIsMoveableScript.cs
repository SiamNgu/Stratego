using UnityEngine;
[CreateAssetMenu (fileName = "StraightCheckTileIsMoveable", menuName = "Scriptable Object/Available Moves/StraightCheckTileIsMoveable")] 

public class StraightTileIsMoveableScript : CheckTileIsMoveableBase
{
    public override bool CheckTileIsMoveable(CellCoordinates tileCoordinates, CellCoordinates pieceCoordinates)
    {
        return tileCoordinates.Row == pieceCoordinates.Row || tileCoordinates.Column == pieceCoordinates.Column;
    }
}
