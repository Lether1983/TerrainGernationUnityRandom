using UnityEngine;
using System;
using System.Collections;

public class PerlinNoise : MonoBehaviour 
{
    private Vector2[,] directionsVectors;

    private int gridCellSize;

    public PerlinNoise(int ArraySize, int gridSize)
    {
        this.gridCellSize = ArraySize / gridSize;

        System.Random random = new System.Random();

        this.directionsVectors = new Vector2[gridSize + 1, gridSize + 1];

        for (int i = 0; i < gridSize+1; i++)
        {
            for (int j = 0; j < gridSize+1; j++)
            {
                this.directionsVectors[i, j] = RandomiseVector(random);
            }
        }
    }

    public float Noise2D(int X, int Y)
    {
        Vector2 point = new Vector2((float)X / gridCellSize, (float)Y / gridCellSize);
        
        float x0 = X / gridCellSize;
        float y0 = Y / gridCellSize;
        float x1 = X + 1;
        float y1 = Y + 1;

        Vector2 GridCornerBottomLeft = new Vector2(x0, y0);
        Vector2 GridCornerBottomRight = new Vector2(x1, x0);
        Vector2 GridCornerTopRight = new Vector2(x1, y1);
        Vector2 GridCornerTopLeft = new Vector2(x0, y1);

        Vector2 FromGridCornerBottomLeftToThePoint = point - GridCornerBottomLeft;
        Vector2 FromGridCornerBottomRightToThePoint = point - GridCornerBottomRight;
        Vector2 FromGridCornerTopRightToThePoint = point - GridCornerTopRight;
        Vector2 FromGridCornerTopLeftToThePoint = point - GridCornerTopLeft;

        float TopLeftCorner = Vector2.Dot(GridCornerTopLeft, FromGridCornerTopLeftToThePoint);
        float TopRightCorner = Vector2.Dot(GridCornerTopRight, FromGridCornerTopRightToThePoint);
        float BottomRightCorner = Vector2.Dot(GridCornerBottomRight, FromGridCornerBottomRightToThePoint);
        float BottomLeftCorner = Vector2.Dot(GridCornerBottomLeft, FromGridCornerBottomLeftToThePoint);


        float SmoothValueX = this.Smooth(point.x-x0);
        float InterpolationAverage = Interpolation(BottomLeftCorner,BottomRightCorner,SmoothValueX);
        float InterpolationSecondAverage = Interpolation(TopLeftCorner, TopRightCorner, SmoothValueX);

        float SmoothValueY = this.Smooth(point.y - y0);
        float InterpolationAverageY = Interpolation(InterpolationAverage, InterpolationSecondAverage, SmoothValueY);

        return InterpolationAverageY;
    }

    public float Smooth(float point)
    {
        return (float)(3 * Math.Pow(point, 2) - 2 * Math.Pow(point, 3));
    }

    public float Interpolation(float average ,float secondAverage ,float maximumOne )
    {
        return secondAverage * maximumOne + average * (1 - maximumOne);
    }

    public Vector2 RandomiseVector(System.Random random)
    {
        Vector2 ranVector = new Vector2((float)random.NextDouble() * 2f - 1f,(float)random.NextDouble() * 2f - 1f);
        return ranVector;
    }
}
