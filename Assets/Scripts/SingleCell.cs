using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCell : MonoBehaviour
{
    public GameObject[] renderers;
    public Vector3 beOut;
    int actualColor = 0;

    // Start is called before the first frame update
    void Start()
    {
        beOut = new Vector3(5f, 0f, 0f);
    }


    public void SetColor(int index)
    {
        renderers[actualColor].transform.localPosition = beOut;
        renderers[index].transform.localPosition = new Vector3();
        actualColor = index;
    }
}
