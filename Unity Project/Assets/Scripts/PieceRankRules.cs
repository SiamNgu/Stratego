using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PieceRankRules", menuName = "Scriptable Object/PieceRankRules"), Serializable]
public class PieceRankRules : ScriptableObject
{
    public string[] pieceIds = new string[0];

    public PieceVsData[] pieceVsDatas = new PieceVsData[0];

    [Serializable]
    public struct PieceVsData
    {
        public string[] vsPieceIds;
        public int winnerIndex;
    }
}