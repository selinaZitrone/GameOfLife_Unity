using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // check if game is running and if game has started
    bool isRunning = false;


    public Slider matrixDimSlide;
    public TMP_Text matrixDimSlideValue;
    public Slider simulationSpeedSlider;
    public TMP_Text simulationSpeedSlideValue;

    // Start is called before the first frame update
    void Start()
    {
        //set default values of slider
        matrixDimSlide.value = 50;
        simulationSpeedSlider.value = 50;
        

    }

    public void StartGame()
    {
        matrixSize = (int)matrixDimSlide.value;
        isRunning = true;
        PrepareRenderer();
        PrepareMatrixes(matrixSize);
        timer = refreshTime;
        actualMatrix = true;
    }

    public void PauseGame()
    {
        switch (isRunning)
        {
            case false:
                isRunning = true;
                break;
            case true:
                isRunning = false;
                break;
        }
    }

    //private void ChangeArray(int[,] toChange)
    //{
    //    toChange[0, 0] = 0;
    //}

    private void PrepareMatrixes(int size)
    {
        // prepare matrix_1
        matrix_1 = new int[size, size];
        matrix_2 = new int[size, size];

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
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
        float refreshModifier = simulationSpeedSlider.value / 100;
        // mit timer
        timer -= Time.deltaTime;
        if (timer < 0 && isRunning)
        {
            SetRenderOutput();
            PlayRound();
            timer = refreshTime * (1- refreshModifier);
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

    

    public void SetMatrixSize()
    {
        SetMatrixSizeTextValue();
    }

    public void SetMatrixSizeTextValue()
    {
        matrixDimSlideValue.SetText(matrixDimSlide.value.ToString());
    }
    
    public void SetSimulationSpeed()
    {
        simulationSpeedSlideValue.SetText(simulationSpeedSlider.value.ToString());
    }   
    
}
