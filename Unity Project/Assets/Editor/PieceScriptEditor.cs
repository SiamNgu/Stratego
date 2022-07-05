using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(PieceScript))]
public class PieceScriptEditor : Editor
{
    [SerializeField] private PieceRankRules pieceRankRules;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Piece ID: " + serializedObject.FindProperty("pieceID"));

        serializedObject.FindProperty("selectedPieceIDIndex").intValue = EditorGUILayout.Popup(serializedObject.FindProperty("selectedPieceIDIndex").intValue, pieceRankRules.pieceIds);
        serializedObject.FindProperty("pieceID").stringValue = pieceRankRules.pieceIds[serializedObject.FindProperty("selectedPieceIDIndex").intValue];
        serializedObject.ApplyModifiedProperties();
    }
}
