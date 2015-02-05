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

    public double Noise2D(int X, int Y)
    {
        Vector2 point = new Vector2((float)X / gridCellSize, (float)Y / gridCellSize);
        
        double x0 = X / gridCellSize;
        double y0 = Y / gridCellSize;
        double x1 = X + 1;
        double y1 = Y + 1;

        Vector2 GridCornerBottomLeft = new Vector2((float)x0,(float)y0);
        Vector2 GridCornerBottomRight = new Vector2((float)x1,(float) x0);
        Vector2 GridCornerTopRight = new Vector2((float)x1,(float) y1);
        Vector2 GridCornerTopLeft = new Vector2((float)x0,(float) y1);

        Vector2 FromGridCornerBottomLeftToThePoint = point - GridCornerBottomLeft;
        Vector2 FromGridCornerBottomRightToThePoint = point - GridCornerBottomRight;
        Vector2 FromGridCornerTopRightToThePoint = point - GridCornerTopRight;
        Vector2 FromGridCornerTopLeftToThePoint = point - GridCornerTopLeft;

        double TopLeftCorner = Vector2.Dot(GridCornerTopLeft, FromGridCornerTopLeftToThePoint);
        double TopRightCorner = Vector2.Dot(GridCornerTopRight, FromGridCornerTopRightToThePoint);
        double BottomRightCorner = Vector2.Dot(GridCornerBottomRight, FromGridCornerBottomRightToThePoint);
        double BottomLeftCorner = Vector2.Dot(GridCornerBottomLeft, FromGridCornerBottomLeftToThePoint);


        double SmoothValueX = this.Smooth(point.x - x0);
        Debug.Log(SmoothValueX);
        double InterpolationAverage = Interpolation(BottomLeftCorner,BottomRightCorner,SmoothValueX);
        double InterpolationSecondAverage = Interpolation(TopLeftCorner, TopRightCorner, SmoothValueX);

        double SmoothValueY = this.Smooth(point.y - y0);
        double InterpolationAverageY = Interpolation(InterpolationAverage, InterpolationSecondAverage, SmoothValueY);

        return InterpolationAverageY;
        Debug.Log(InterpolationAverageY);
    }

    public double Smooth(double point)
    {
        return (3 * Math.Pow(point, 2) - 2 * Math.Pow(point, 3));
    }

    public double Interpolation(double average ,double secondAverage ,double maximumOne )
    {
        return secondAverage * maximumOne + average * (1 - maximumOne);
    }

    public Vector2 RandomiseVector(System.Random random)
    {
        Vector2 ranVector = new Vector2((float)random.NextDouble() * 2f - 1f,(float)random.NextDouble() * 2f - 1f);
        return ranVector;
    }
}
