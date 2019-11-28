using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitrus_Genenator_2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Texture2D texture;
    Sprite mySprite;
    int textureWidth = 256;
    int textureHeight = 256;

    public Color[] colors;
    //public Color[] colors2;
    //int actualColor = 0;

    [Header("Game of Live vars")]
    public int matrixSize = 32;
    public int cellSize = 4;

    int[,] matrix_1;
    int[,] matrix_2;
    int[,] toRender;

    // definiert welche matrix zur Updatefunktion als Referenz und welche als Output weitergegeben wird
    bool actualMatrix = true;

    // Start is called before the first frame update
    void Start()
    {
        PrepareRenderer();
        Restart();
        //matrix_1 = new int[,] { {1 , 0 }, {0 , 1 } };
        //matrix_2 = matrix_1;
        //ChangeArray(matrix_2);
        //Debug.Log(matrix_1[0, 0]);


    }

    public void Restart()
    {
        PrepareMatrixes();
        timer = refreshTime;
        actualMatrix = true;
    }

    //private void ChangeArray(int[,] toChange)
    //{
    //    toChange[0, 0] = 0;
    //}

    private void PrepareMatrixes()
    {
        // prepare matrix_1
        matrix_1 = new int[matrixSize, matrixSize];
        matrix_2 = new int[matrixSize, matrixSize];

        for (int x = 0; x < matrixSize; x++)
        {
            for (int y = 0; y < matrixSize; y++)
            {
                matrix_1[x, y] = UnityEngine.Random.Range(0, 2);
            }
        }

        toRender = matrix_1;
        SetRenderOutput();
    }



    private void PrepareRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textureWidth = matrixSize * cellSize;
        textureHeight = matrixSize * cellSize;
        texture = new Texture2D(textureWidth, textureHeight);
        Rect myRect = new Rect(0, 0, textureWidth, textureHeight);
        mySprite = Sprite.Create(texture, myRect, new Vector2(.5f, .5f));
        spriteRenderer.sprite = mySprite;
    }

    public float refreshTime = 1f;
    float timer;

    // Update is called once per frame
    void Update()
    {
        // mit timer
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            SetRenderOutput();
            PlayRound();
            timer = refreshTime;
        }
    }

    private void PlayRound()
    {
        if (actualMatrix)
        {
            GameOfLife.UpdateCellStatus(matrix_1, matrix_2);
            // funzione con matrix 1
            toRender = matrix_2;
        } else
        {
            GameOfLife.UpdateCellStatus(matrix_2, matrix_1);
            // funzione con matrix 2
            toRender = matrix_1;
        }

        actualMatrix = !actualMatrix;
    }

    private void FakeGame(int[,] intro, int[,] outro)
    {

    }

    private void ColorCell(int ox, int oy, Color color)
    {
        for (int x = ox * cellSize; x < ox * cellSize + cellSize; x++)
        {
            for (int y = oy * cellSize; y < oy * cellSize + cellSize; y++)
            {
                texture.SetPixel(x, y, color);
            }
        }
    }

    private void SetRenderOutput()
    {
        // cell Size e toRender

        for(int x = 0; x < matrixSize; x++)
        {
            for (int y = 0; y < matrixSize; y++)
            {

                if (toRender[x, y] == 1)
                {
                    ColorCell(x, y, colors[1]);
                }
                else
                {
                    ColorCell(x, y, colors[0]);
                }
            }

        }


        //for (int y = 1; y < texture.height; y = y + 2)
        //{
        //    for (int x = 1; x < texture.width; x = x + 2)
        //    {
        //        Color color = colors[actualColor];
        //        texture.SetPixel(x, y, color);
        //    }
        //}

        //for (int y = 0; y < texture.height; y = y + 2)
        //{
        //    for (int x = 0; x < texture.width; x = x + 2)
        //    {
        //        Color color = colors2[actualColor];
        //        texture.SetPixel(x, y, color);
        //    }
        //}

        texture.Apply();
    }
}
