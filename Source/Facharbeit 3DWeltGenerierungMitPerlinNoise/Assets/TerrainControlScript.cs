using UnityEngine;
using System.Collections;

public enum Algorithmen { PerlinNoise, MathFPerlinNoise }

public class TerrainControlScript : MonoBehaviour
{
    
    public Algorithmen algorithmen { get; set; }
    public int MaxHeight;
    public int Lenght;
    public int Width;
    public int HightmapResolution;
    PerlinNoise Noise;
    private float divRange;

    public void GenerateTerrainPerlinNoise()
    {
        TerrainData tdata = this.gameObject.GetComponent<Terrain>().terrainData;
        tdata.size = new Vector3(Width, MaxHeight, Lenght);
        Noise = new PerlinNoise(tdata.heightmapResolution, tdata.heightmapResolution);
        float[,] height = new float[tdata.heightmapResolution, tdata.heightmapResolution];

        for (int i = 0; i < tdata.heightmapResolution; i++)
        {
            for (int j = 0; j < tdata.heightmapResolution; j++)
            {
                float Heightmapvalue = Noise.NoiseFunction(tdata.heightmapResolution);

                height[i, j] = Heightmapvalue * 0.009f;
            }
        }

        for (int l = 0; l < 30; l++)
        {
            height[Random.Range(0, 33), Random.Range(0, 33)] /= 0.01f;
        }

        tdata.SetHeights(0, 0, height);
    }
}