using UnityEditor;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
[CustomEditor(typeof(PieceRankRules))]
public class PieceRankRuleEditor : Editor
{
    private float cellWidth = 100;

    private SerializedProperty pieceIds;

    private SerializedProperty pieceVsDatas;

    private void OnEnable()
    {
        pieceIds = serializedObject.FindProperty("pieceIds");
        pieceVsDatas = serializedObject.FindProperty("pieceVsDatas");
    }

    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        base.OnInspectorGUI();

        Vector2 matrixScrollPos = Vector2.zero;
        cellWidth = EditorGUILayout.Slider(cellWidth, 0, 150);

        matrixScrollPos = EditorGUILayout.BeginScrollView(matrixScrollPos, true, false, GUILayout.Width(EditorGUIUtility.currentViewWidth - 40), GUILayout.Height(100));
        EditorGUILayout.BeginHorizontal();
        for (int i = -1; i < pieceIds.arraySize - 1; i++)
        {
            if (i == -1)
            {
                EditorGUILayout.LabelField("Winner", GUILayout.Width(100));
                continue;
            }
            EditorGUILayout.LabelField(pieceIds.GetArrayElementAtIndex(pieceIds.arraySize - 1 - i).stringValue, GUILayout.Width(cellWidth));
        }
        EditorGUILayout.EndHorizontal();

        pieceVsDatas.arraySize = pieceIds.arraySize;

        for (int r = 0; r < pieceIds.arraySize - 1; r++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(pieceIds.GetArrayElementAtIndex(r).stringValue, GUILayout.Width(100));
  
            for (int c = 0; c < pieceIds.arraySize - r - 1; c++)
            {
                int popupSelectedIndex = pieceVsDatas.GetArrayElementAtIndex(r * (pieceIds.arraySize - 1) + c).FindPropertyRelative("winnerIndex").intValue;
                string[] options = new string[2] { pieceIds.GetArrayElementAtIndex(r).stringValue, pieceIds.GetArrayElementAtIndex(pieceIds.arraySize - 1 - c).stringValue };
                popupSelectedIndex = EditorGUILayout.Popup(popupSelectedIndex, options, GUILayout.Width(cellWidth));
                pieceVsDatas.GetArrayElementAtIndex(r * (pieceIds.arraySize - 1) + c).FindPropertyRelative("vsPieceIds").arraySize = options.Length;
                pieceVsDatas.GetArrayElementAtIndex(r * (pieceIds.arraySize - 1) + c).FindPropertyRelative("vsPieceIds").GetArrayElementAtIndex(0).stringValue = options[0];
                pieceVsDatas.GetArrayElementAtIndex(r * (pieceIds.arraySize - 1) + c).FindPropertyRelative("vsPieceIds").GetArrayElementAtIndex(1).stringValue = options[1];
                pieceVsDatas.GetArrayElementAtIndex(r * (pieceIds.arraySize - 1) + c).FindPropertyRelative("winnerIndex").intValue = popupSelectedIndex;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        serializedObject.ApplyModifiedProperties();

    }
}
