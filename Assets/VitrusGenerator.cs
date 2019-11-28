using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitrusGenerator : MonoBehaviour
{
    public GameObject singleCell;
    public int width;
    public int height;
    private List<SingleCell> cells;
    public Vector3 startPosition;
    public float cellDimension;

    public int actualColor = 0;

    private void prepareTheVitrus()
    {
        for (int i = 0; i < width; i++)
        {
            for (int e = 0; e < height; e++)
            {
                GameObject newCell = Instantiate(singleCell, gameObject.transform);
                cells.Add(newCell.GetComponent<SingleCell>());
                newCell.transform.position = startPosition + new Vector3(cellDimension*i, -cellDimension*e, 0f);
            }
        }
    }

    private void rainBow()
    {
        actualColor++;
        if(actualColor == cells[0].renderers.Length)
        {
            actualColor = 0;
        }
        foreach (var cell in cells)
        {
            cell.SetColor(actualColor);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        cells = new List<SingleCell>();

        prepareTheVitrus();
    }

    float Timer = 5f;

    // Update is called once per frame
    void Update()
    {

            rainBow();

    }


}
