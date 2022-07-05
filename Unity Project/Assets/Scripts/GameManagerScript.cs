using UnityEngine;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance { get; private set; }

    public CellCoordinates activeTileCoordinates;

    [field: SerializeField]
    public Camera cam { get; private set; }

    //Mouse interaction with Gameobjects
    public RaycastHit mouseRaycastHit;
    [SerializeField] private LayerMask mouseLayerMask;

    //Board data
    [SerializeField] private GameObject tile;
    public const int boardRows = 2;
    public const int boardColumns = 2;
    public TileData[,] tileDatas { get; private set; }

    //spawning pieces
    [System.Serializable]
    public struct pieceToSpawnData
    {
        public CellCoordinates cellCoordinate;
        public GameObject pieceGO;
    }
    [SerializeField] private pieceToSpawnData[] pieceToSpawnDatas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //spawning tiles
        Vector2 tileRendererBoundsSize = tile.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 tileStartSpawnPos = new Vector2(
            -tileRendererBoundsSize.x * boardRows / 2, 
            tileRendererBoundsSize.y * boardColumns / 2
            );
        Vector2 tileCurrentSpawnPos = tileStartSpawnPos;
        tileDatas = new TileData[boardRows , boardColumns];
        for (int r = 0; r < boardRows; r++)
        {
            tileCurrentSpawnPos = new Vector2(tileStartSpawnPos.x, tileCurrentSpawnPos.y - tileRendererBoundsSize.y);
            for (int c = 0; c < boardColumns; c++)
            {
                GameObject instantiatedTile = Instantiate(tile, tileCurrentSpawnPos, Quaternion.identity);
                instantiatedTile.transform.name = "Tile Row " + r + " Column " + c;
                tileDatas[r, c] = new TileData(instantiatedTile.transform.position, instantiatedTile);
                tileCurrentSpawnPos += Vector2.right * tileRendererBoundsSize.x;
            }
        }

        //spawning pieces
        for (int i = 0; i < pieceToSpawnDatas.Length; i++)
        {
            CellCoordinateElement instantiatedPiece = Instantiate(pieceToSpawnDatas[i].pieceGO).GetComponent<CellCoordinateElement>();
            instantiatedPiece.cellCoordinates = pieceToSpawnDatas[i].cellCoordinate;
            instantiatedPiece.MoveToCell(pieceToSpawnDatas[i].cellCoordinate);
        }
    }
}