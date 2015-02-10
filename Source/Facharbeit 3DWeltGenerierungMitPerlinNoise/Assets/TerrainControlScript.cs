using UnityEngine;
using System.Collections;



public class TerrainControlScript : MonoBehaviour
{
    public enum Algorithmen { PerlinNoise }
    public Algorithmen algorithmen { get; set; }
    public int MaxHeight;
    public int Lenght;
    public int Width;
    PerlinNoise Noise;
    private float divRange;


    //public void GenerateTerrainPerlinNoise(float tileSize)
    //{
    //    divRange = Random.Range(1, 2);
    //    TerrainData tdata = this.gameObject.GetComponent<Terrain>().terrainData;
    //    tdata.size = new Vector3(Width, MaxHeight, Lenght);
    //    tdata.heightmapResolution = 2049;
    //    Noise = new PerlinNoise(tdata.heightmapResolution, 10);
    //    //Heights For Our Hills/Mountains
    //    float[,] hts = new float[tdata.heightmapWidth, tdata.heightmapHeight];
    //    for (int i = 0; i < tdata.heightmapWidth; i++)
    //    {
    //        for (int k = 0; k < tdata.heightmapHeight; k++)
    //        {
    //            hts[i, k] = Mathf.PerlinNoise(((float)i / (float)tdata.heightmapWidth) * tileSize, ((float)k / (float)tdata.heightmapHeight) * tileSize) / divRange;
    //        }
    //    }
    //    this.gameObject.GetComponent<Terrain>().terrainData.SetHeights(0, 0, hts);
    //}

    public void GenerateTerrainPerlinNoise()
    {
        TerrainData tdata = this.gameObject.GetComponent<Terrain>().terrainData;
        tdata.size = new Vector3(Width, MaxHeight, Lenght);
        tdata.heightmapResolution = 2049;
        Noise = new PerlinNoise(tdata.heightmapResolution, 10);
        float[,] heigh = new float[tdata.heightmapWidth, tdata.heightmapHeight];
        float xLength = tdata.size.x / tdata.size.x * 2049;
        float zLength = tdata.size.z / tdata.size.z * 2049;

        for (int i = 0; i < xLength; i++)
        {
            for (int j = 0; j < zLength; j++)
            {
                double Penis = Noise.Noise2D(i, j);

                double transformedValue = (Penis + 1) / 2;

                heigh[i, j] = (float)transformedValue;
            }
        }

        tdata.SetHeights(0, 0, heigh);
    }
}