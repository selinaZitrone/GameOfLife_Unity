using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOfLife
{
    
        //int[,] statusArray = new int[5, 5] { { 1,0,1,0,0}, { 1, 0, 1, 0, 0 } , { 1, 0, 1, 0, 0 } , { 1, 0, 1, 0, 0 } , { 1, 0, 1, 0, 0 } };
        //int[,] toRenderArray = new int[5, 5] { { 1, 0, 1, 0, 0 }, { 1, 0, 1, 0, 0 }, { 1, 0, 1, 0, 0 }, { 1, 0, 1, 0, 0 }, { 1, 0, 1, 0, 0 } };
        
    
         
    public static void UpdateCellStatus(int[,] status, int[,] toRender)
    {
        int arraySizeX = status.GetLength(0);
        int arraySizeY = status.GetLength(1);
        //Debug.Log(arraySizeX);
        //Debug.Log(arraySizeY);
        //loop through status array and check neighbors of each cell
        for(int j = 0; j < arraySizeX; j++) //columns
        {
            for(int i = 0; i < arraySizeY; i++) // rows
            {
                int sumOfNeighbors = 0;
                //calculate number of alive neighbors
                for(int x = Math.Max(j-1, 0); x < Math.Min(j+2, arraySizeX); x++) //columns of neighbors
                {
                    for(int y = Math.Max(i-1, 0); y < Math.Min(i+2, arraySizeY); y++){
                        sumOfNeighbors += status[y, x];
                    }
                }

                //find out the new status of the respective cell:
                int cellStatus = status[i, j];
                sumOfNeighbors -= cellStatus; //subtract cell status because it was part of the sum of neighbors

                toRender[i, j] = cellStatus;

                switch (cellStatus)
                {
                    case 1:
                        switch (sumOfNeighbors)
                        {
                            case 2:
                                break;
                            case 3:
                                break;
                            default:
                                toRender[i, j] = 0;
                                break;
                        }
                        break;
                    case 0:
                        switch (sumOfNeighbors)
                        {
                            case 3:
                                toRender[i, j] = 1;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;

                }
            }
        }
        //PrintArray(toRender);
    }

    private static void PrintArray(int[,] arr)
    {
        int rowLength = arr.GetLength(0);
        int colLength = arr.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", arr[i, j]));
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }
        Console.ReadLine();
    }
}
