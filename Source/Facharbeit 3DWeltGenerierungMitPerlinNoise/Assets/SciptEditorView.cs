using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(TerrainControlScript))]
public class SciptEditorView : Editor
{
    public override void OnInspectorGUI()
    {
        TerrainControlScript terrainControlScript = (TerrainControlScript)target;

        EditorGUILayout.IntField("Width", terrainControlScript.Width);
        EditorGUILayout.IntField("Lenght", terrainControlScript.Lenght);
        EditorGUILayout.IntField("Max Height", terrainControlScript.MaxHeight);

        EditorGUILayout.EnumPopup("Algorithmen",terrainControlScript.algorithmen);

        if(GUILayout.Button("Generate Terrain"))
        {

        }
    }
}
