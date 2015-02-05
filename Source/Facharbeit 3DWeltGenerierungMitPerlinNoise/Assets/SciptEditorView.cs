using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(TerrainControlScript))]
public class SciptEditorView : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainControlScript terrainControlScript = (TerrainControlScript)target;

        terrainControlScript.Width = EditorGUILayout.IntField("Width", terrainControlScript.Width);
        terrainControlScript.Lenght = EditorGUILayout.IntField("Lenght", terrainControlScript.Lenght);
        terrainControlScript.MaxHeight = EditorGUILayout.IntField("Max Height", terrainControlScript.MaxHeight);

        EditorGUILayout.EnumPopup("Algorithmen",terrainControlScript.algorithmen);

        if(GUILayout.Button("Generate Terrain"))
        {
            terrainControlScript.GenerateTerrain();
        }
    }
}
