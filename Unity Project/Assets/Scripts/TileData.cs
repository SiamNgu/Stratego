using UnityEngine;

public class TileData
{
    public Vector2 worldPos { get; private set; }
    public GameObject tileGO;
    public PieceScript containingPiece;

    public TileData(Vector2 worldPos, GameObject tileGO)
    {
        this.worldPos = worldPos;
        this.tileGO = tileGO;
    }
}