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

    void OnEnable()
    {
        Noise = new PerlinNoise()
    }
    public void GenerateTerrain()
    {
        float[,] heigh = new float[Width,Lenght];

        heigh[10,10] = 1f;
        TerrainData tdata = this.gameObject.GetComponent<Terrain>().terrainData;
        tdata.size = new Vector3(Width, MaxHeight, Lenght);
        tdata.heightmapResolution = 1001;
        tdata.SetHeights(0, 0, heigh);
    }
}

// ArgumentException: X or Y base out of bounds. Setting up to 1150x1150 while map size is 1025x1025