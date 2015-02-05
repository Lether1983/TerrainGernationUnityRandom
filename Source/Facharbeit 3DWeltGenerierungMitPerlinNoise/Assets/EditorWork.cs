using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorWork : Editor
{
    [MenuItem("GameObject/Create Other/Terrain (Random)")]
    static void Init()
    {
        GameObject game;
        TerrainData terdata = new TerrainData();
        string name = "New TerrainData";
        AssetDatabase.CreateAsset(terdata, "assets/" + name + ".assets");
        game = Terrain.CreateTerrainGameObject(terdata);
        game.AddComponent<TerrainControlScript>();
    }
}