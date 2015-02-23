using UnityEngine;
using System.Collections;
using System.Linq;

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
    private TerrainData tdata;
    private float[, ,] alphaData;
    private const int Sand = 0;
    private const int Stone = 1;
    private const int Grass = 2;
    private const int Water = 3;
    private const int Resolution = 32;

    public void GenerateTerrainPerlinNoise()
    {
        tdata = this.gameObject.GetComponent<Terrain>().terrainData;
        tdata.alphamapResolution = Resolution;
        tdata.baseMapResolution = Resolution;
        tdata.size = new Vector3(Width, MaxHeight, Lenght);
        Noise = new PerlinNoise(tdata.heightmapResolution, tdata.heightmapResolution);
        float[,] height = new float[tdata.heightmapResolution, tdata.heightmapResolution];

        for (int i = 0; i < tdata.heightmapResolution; i++)
        {
            for (int j = 0; j < tdata.heightmapResolution; j++)
            {
                float Heightmapvalue = Noise.NoiseFunction(tdata.heightmapResolution);
                height[i, j] = Heightmapvalue * 0.01f;
            }
        }

        for (int l = 0; l < 30; l++)
        {
            height[Random.Range(0, 33), Random.Range(0, 33)] /= 0.01f;
        }
        tdata.SetHeights(0, 0, height);
        TerrainPainter();
    }

    public void TerrainPainter()
    {
        alphaData = new float[tdata.alphamapWidth, tdata.alphamapHeight, tdata.alphamapLayers];

        for (int z = 0; z < tdata.alphamapHeight; z++)
        {
            for (int x = 0; x < tdata.alphamapWidth; x++)
            {
                float ZCordinate = (float)z / tdata.alphamapHeight;
                float XCordinate = (float)x / tdata.alphamapWidth;

                float height = tdata.GetHeight(Mathf.RoundToInt(ZCordinate * tdata.heightmapHeight), Mathf.RoundToInt(XCordinate *tdata.heightmapWidth));

                Vector3 vectorNormal = tdata.GetInterpolatedNormal(ZCordinate, XCordinate);

                float steepness = tdata.GetSteepness(ZCordinate, XCordinate);

                float[] TextureWeights = new float[tdata.alphamapLayers];

                TextureWeights[0] = 0.5f;

                TextureWeights[2] = Mathf.Clamp01((steepness * steepness) / tdata.heightmapHeight - height);

                TextureWeights[1] = height * Mathf.Clamp01(vectorNormal.y);

                TextureWeights[3] = 1.0f - Mathf.Clamp01((steepness * steepness) / (tdata.heightmapHeight / 5f));

                float YCordinate = TextureWeights.Sum();

                for (int i = 0; i < tdata.alphamapLayers; i++)
                {
                    TextureWeights[i] /= YCordinate;
                    alphaData[x, z, i] = TextureWeights[i];
                }
            }
        }
        tdata.SetAlphamaps(0, 0, alphaData);
    }
}

