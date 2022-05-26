using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasControl : MonoBehaviour
{
    public GameObject[] canvases;
    public int activeCanvasIndex;

    public enum canvasesEnum { CAT } // Find out how to use this

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject c in canvases)
        {
            if (c == canvases[activeCanvasIndex])
            {
                c.SetActive(true); 
            } else
            {
                c.SetActive(false);
            }
        }
    }

    public void UpdateCanvasActive(int newCanvasIndex)
    {
        foreach (GameObject c in canvases)
        {
            if (c == canvases[newCanvasIndex])
            {
                c.SetActive(true);
            }
            else
            {
                c.SetActive(false);
            }
        }
    }
    
}
