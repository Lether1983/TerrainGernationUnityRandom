using UnityEngine;
using System;
using System.Collections;

public class PerlinNoise
{
    private Vector2[,] directionsVectors;

    private float gridCellSize;

    public PerlinNoise(int ArraySize, int gridSize)
    {
        this.gridCellSize = (float)ArraySize /(float)gridSize;

        this.directionsVectors = new Vector2[gridSize + 1,gridSize + 1];

        for (int i = 0; i < gridSize+1; i++)
        {
            for (int j = 0; j < gridSize+1; j++)
            {
                this.directionsVectors[i, j] = RandomiseVector();
            }
        }
    }

    public float Noise2D(int X, int Y)
    {
        Vector2 point = new Vector2((float)X / gridCellSize, (float)Y / gridCellSize);
        
       float x0 = X / gridCellSize;
       float y0 = Y / gridCellSize;
       float x1 = x0 + 1;
       float y1 = y0 + 1;

        Vector2 GridCornerBottomLeft = this.directionsVectors[(int)x0, (int)y0];
        Vector2 GridCornerBottomRight = this.directionsVectors[(int)x1,(int)x0];
        Vector2 GridCornerTopRight = this.directionsVectors[(int)x1,(int)y1];
        Vector2 GridCornerTopLeft = this.directionsVectors[(int)x0,(int)y1];

        Vector2 FromGridCornerBottomLeftToThePoint = point - GridCornerBottomLeft;
        Vector2 FromGridCornerBottomRightToThePoint = point - GridCornerBottomRight;
        Vector2 FromGridCornerTopRightToThePoint = point - GridCornerTopRight;
        Vector2 FromGridCornerTopLeftToThePoint = point - GridCornerTopLeft;

        float TopLeftCorner = Vector2.Dot(GridCornerTopLeft, FromGridCornerTopLeftToThePoint);
        float TopRightCorner = Vector2.Dot(GridCornerTopRight, FromGridCornerTopRightToThePoint);
        float BottomRightCorner = Vector2.Dot(GridCornerBottomRight, FromGridCornerBottomRightToThePoint);
        float BottomLeftCorner = Vector2.Dot(GridCornerBottomLeft, FromGridCornerBottomLeftToThePoint);


        float SmoothValueX = this.Smooth(point.x - x0);
        float InterpolationAverage = Interpolation(BottomLeftCorner,BottomRightCorner,SmoothValueX);
        float InterpolationSecondAverage = Interpolation(TopLeftCorner, TopRightCorner, SmoothValueX);
        
        float SmoothValueY = this.Smooth(point.y - y0);
        float InterpolationAverageY = Interpolation(InterpolationAverage, InterpolationSecondAverage, SmoothValueY);
        
        return InterpolationAverageY;
    }

    public float Smooth(double point)
    {
        return (float)(3 * Math.Pow(point, 2) - 2 * Math.Pow(point, 3));
    }

    public float Interpolation(float average ,float secondAverage ,float maximumOne )
    {
        return secondAverage * maximumOne + average * (1 - maximumOne);
    }

    public Vector2 RandomiseVector()
    {
        Vector2 ranVector = new Vector2((float)UnityEngine.Random.Range(0f, 1f) * 2f - 1f, (float)UnityEngine.Random.Range(0f, 1f) * 2f - 1f);
        return ranVector;
    }
}
