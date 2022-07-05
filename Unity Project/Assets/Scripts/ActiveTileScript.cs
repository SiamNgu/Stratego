using UnityEngine;

public class ActiveTileScript : CellCoordinateElement
{
    private void Update()
    {
        //setting active tile position to tile that is closest to mouse
        Vector2 mouseWorldPos = new Vector2(GameManagerScript.Instance.cam.ScreenToWorldPoint(Input.mousePosition).x, GameManagerScript.Instance.cam.ScreenToWorldPoint(Input.mousePosition).y);
        float minDistMouseToTile = 0;
        CellCoordinates closestTileToMouseCoordinates = new CellCoordinates(0, 0);

        for (int r = 0; r < GameManagerScript.Instance.tileDatas.GetLength(0); r++)
        {
            for (int c = 0; c < GameManagerScript.Instance.tileDatas.GetLength(1); c++)
            {
                if (c == 0 && r == 0)
                {
                    minDistMouseToTile = Vector2.Distance(mouseWorldPos, GameManagerScript.Instance.tileDatas[r, c].worldPos);
                    closestTileToMouseCoordinates = new CellCoordinates(r, c);
                    continue;
                }
                if (Vector2.Distance(mouseWorldPos, GameManagerScript.Instance.tileDatas[r, c].worldPos) < minDistMouseToTile)
                {
                    minDistMouseToTile = Vector2.Distance(mouseWorldPos, GameManagerScript.Instance.tileDatas[r, c].worldPos);
                    closestTileToMouseCoordinates = new CellCoordinates(r, c);
                }
            }
        }
        MoveToCell(closestTileToMouseCoordinates);
        GameManagerScript.Instance.activeTileCoordinates = cellCoordinates;
    }
}