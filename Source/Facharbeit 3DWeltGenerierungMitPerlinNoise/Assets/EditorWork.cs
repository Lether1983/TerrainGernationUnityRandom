using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorWork : Editor
{
    [MenuItem("GameObject/Create Other/Terrain (Random)")]
    static void Init()
    {
        GameObject game;
        float[,]heigh = new float[5,5];
        heigh[0, 0] = 1f;
        heigh[1, 1] = 0.5f;
        heigh[3, 3] = 1f;
        TerrainData terdata = new TerrainData();
        terdata.SetHeights(0,0,heigh);
        terdata.size = new Vector3(100, 100, 10);
        string name = "New TerrainData";
        AssetDatabase.CreateAsset(terdata, "assets/" + name + ".assets");
        game = Terrain.CreateTerrainGameObject(terdata);
        game.AddComponent<TerrainControlScript>();
    }
}