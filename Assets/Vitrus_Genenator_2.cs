using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitrus_Genenator_2 : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    Texture2D texture;
    Sprite mySprite;
    public int textureWidth = 256;
    public int textureHeight = 256;

    public Color[] colors;
    public Color[] colors2;
    int actualColor = 0;

    [Header("Game of Live vars")]
    public int matrixSize = 32;
    public int cellSize = 4;

    int[,] matrix_1;
    int[,] matrix_2;

    // Start is called before the first frame update
    void Start()
    {
        PrepareRenderer();
        PrepareMatrixes();
        
    }

    private void PrepareMatrixes()
    {
        // prepare matrix_1 & 2
    }

    private void PrepareRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        texture = new Texture2D(textureWidth, textureHeight);
        Rect myRect = new Rect(0, 0, textureWidth, textureHeight);
        mySprite = Sprite.Create(texture, myRect, new Vector2(.5f, .5f));
        spriteRenderer.sprite = mySprite;
    }

    // Update is called once per frame
    void Update()
    {
        // mit timer
        PlayRound();
        SetRenderOutput();
    }

    private void PlayRound()
    {
        // GAME OF LIFE
    }

    private void SetRenderOutput()
    {
        actualColor++;

        if (actualColor == colors.Length)
        {
            actualColor = 0;
        }


        //for (int y = 1; y < texture.height; y = y + 2)
        //{
        //    for (int x = 1; x < texture.width; x = x + 2)
        //    {
        //        Color color = colors[actualColor];
        //        texture.SetPixel(x, y, color);
        //    }
        //}

        for (int y = 0; y < texture.height; y = y + 2)
        {
            for (int x = 0; x < texture.width; x = x + 2)
            {
                Color color = colors2[actualColor];
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
    }
}
