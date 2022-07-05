using UnityEngine;
using System;
using System.Linq;

public class PieceScript : CellCoordinateElement
{
    [SerializeField] private CheckTileIsMoveableBase checkTileIsMoveableScript;
    private CellCoordinates cellCoordinatesBeforeDrag;

    [SerializeField] private PieceRankRules pieceRankRules;

    [HideInInspector] public int selectedPieceIDIndex = 0;
    [HideInInspector] public string pieceID;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cellCoordinatesBeforeDrag = cellCoordinates;
            for (int r = 0; r < GameManagerScript.boardRows; r++)
            {
                for (int c = 0; c < GameManagerScript.boardColumns; c++)
                {
                    if (checkTileIsMoveableScript.CheckTileIsMoveable(new CellCoordinates(r, c), cellCoordinates))
                    {
                        GameManagerScript.Instance.tileDatas[r, c].tileGO.GetComponent<Animator>().SetBool("ShowMoveable", true);
                    }
                }
            }
        }
    }

    private void OnMouseDrag()
    {
        transform.position = GameManagerScript.Instance.cam.ScreenToWorldPoint(Input.mousePosition) * Vector2.one;
        transform.position = new Vector2(
            Mathf.Clamp(
                transform.position.x, 
                GameManagerScript.Instance.tileDatas[0, 0].worldPos.x, 
                GameManagerScript.Instance.tileDatas[0, GameManagerScript.Instance.tileDatas.GetLength(1) - 1].worldPos.x
                ),
            Mathf.Clamp(
                transform.position.y, 
                GameManagerScript.Instance.tileDatas[GameManagerScript.Instance.tileDatas.GetLength(0) - 1, 0].worldPos.y,
                GameManagerScript.Instance.tileDatas[0, 0].worldPos.y
                )
            );
    }

    private void OnMouseUp()
    {
        if (checkTileIsMoveableScript.CheckTileIsMoveable(GameManagerScript.Instance.activeTileCoordinates, cellCoordinates)) MoveToCell(GameManagerScript.Instance.activeTileCoordinates);
        else MoveToCell(cellCoordinatesBeforeDrag);
        for (int r = 0; r < GameManagerScript.boardRows; r++)
        {
            for (int c = 0; c < GameManagerScript.boardColumns; c++)
            {
                GameManagerScript.Instance.tileDatas[r, c].tileGO.GetComponent<Animator>().SetBool("ShowMoveable", false);
            }
        }
    }

    public override void MoveToCell(CellCoordinates toCellCoordinates)
    {
        GameManagerScript.Instance.tileDatas[cellCoordinates.Row, cellCoordinates.Column].containingPiece = null;
        PieceScript toCellContainingPiece = GameManagerScript.Instance.tileDatas[toCellCoordinates.Row, toCellCoordinates.Column].containingPiece;
        
        if (toCellContainingPiece != null)
        {
            GameManagerScript.Instance.tileDatas[cellCoordinates.Row, cellCoordinates.Column].containingPiece = null;
            int indexOfpieceVsData = Array.FindIndex(pieceRankRules.pieceVsDatas, pieceVsData => pieceVsData.vsPieceIds.Aggregate(0, (acc, x) => (x == pieceID || x == toCellContainingPiece.pieceID) ? ++acc : acc) == 2);
            //if this piece same rank then destroy this piece and other piece
            if (pieceID == toCellContainingPiece.pieceID)
            {
                Destroy(gameObject);
                Destroy(toCellContainingPiece.gameObject);
            }

            //if this piece higher rank then destroy other piece
            else if (pieceRankRules.pieceVsDatas[indexOfpieceVsData].vsPieceIds[pieceRankRules.pieceVsDatas[indexOfpieceVsData].winnerIndex] == pieceID)
            {
                Debug.Log("this piece wins");
                Destroy(toCellContainingPiece.gameObject);
                toCellContainingPiece = this;
            }

            //if other piece wins then destroy this piece
            else if (pieceRankRules.pieceVsDatas[indexOfpieceVsData].vsPieceIds[pieceRankRules.pieceVsDatas[indexOfpieceVsData].winnerIndex] == toCellContainingPiece.pieceID)
            {
                Debug.Log("this piece loses");
                Destroy(gameObject);
            }
        }
        else if (toCellContainingPiece == null)
        {
            GameManagerScript.Instance.tileDatas[toCellCoordinates.Row, toCellCoordinates.Column].containingPiece = this;
        }
        base.MoveToCell(toCellCoordinates);
    }

}
