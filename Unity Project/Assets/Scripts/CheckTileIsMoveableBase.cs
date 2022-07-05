public abstract class CheckTileIsMoveableBase : UnityEngine.ScriptableObject
{
    public abstract bool CheckTileIsMoveable(CellCoordinates tileCoordinates, CellCoordinates pieceCoordinates);
}
