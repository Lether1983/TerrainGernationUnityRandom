using UnityEngine;
using System.Collections;



public class TerrainControlScript : MonoBehaviour
{
    public enum Algorithmen { PerlinNoise }
    public Algorithmen algorithmen { get; set; }
    public int MaxHeight;
    public int Lenght;
    public int Width;

    
    public void GenerateTerrain()
    {

    }
}
