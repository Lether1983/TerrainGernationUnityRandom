using UnityEngine;
using System.Collections;



public class TerrainControlScript : MonoBehaviour
{
    public enum Algorithmen { PerlinNoise , MathFPerlinNoise }
    public Algorithmen algorithmen { get; set; }
    public int MaxHeight;
    public int Lenght;
    public int Width;
    public int HightmapResolution;
    PerlinNoise Noise;
    private float divRange;


    public void GenerateTerrainPerlinNoise(float tileSize)
    {
        divRange = Random.Range(1, 2);
        TerrainData tdata = this.gameObject.GetComponent<Terrain>().terrainData;
        tdata.size = new Vector3(Width, MaxHeight, Lenght);
        tdata.heightmapResolution = 33;
        Noise = new PerlinNoise(tdata.heightmapResolution, 10);
        //Heights For Our Hills/Mountains
        float[,] hts = new float[tdata.heightmapWidth, tdata.heightmapHeight];
        for (int i = 0; i < tdata.heightmapWidth; i++)
        {
            for (int k = 0; k < tdata.heightmapHeight; k++)
            {
                hts[i, k] = Mathf.PerlinNoise(((float)i / (float)tdata.heightmapWidth) * tileSize, ((float)k / (float)tdata.heightmapHeight) * tileSize) / divRange;
            }
        }
        this.gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, hts);
    }

    //public void GenerateTerrainPerlinNoise()
    //{
    //    TerrainData tdata = this.gameObject.GetComponent<Terrain>().terrainData;
    //    tdata.size = new Vector3(Width, MaxHeight, Lenght);
    //    //tdata.heightmapResolution = HightmapResolution;
    //    Noise = new PerlinNoise(tdata.heightmapResolution,33);
    //    float[,] height = new float[tdata.heightmapResolution, tdata.heightmapResolution];

    //    for (int i = 0; i < tdata.heightmapResolution; i++)
    //    {
    //        for (int j = 0; j < tdata.heightmapResolution; j++)
    //        {
    //            float Penis = Noise.NoiseFunction(tdata.heightmapResolution);

    //            height[i, j] = Penis;
    //            //Debug.Log(height[i, j]);
    //        }
    //    }

        
    //    tdata.SetHeights(0, 0, height);
    //}
}