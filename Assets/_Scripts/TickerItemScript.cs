using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickerItemScript : MonoBehaviour
{
    float tickerWidth;
    float pixelsPerSecond;
    RectTransform rt;
    Canvas canvasUsed;

    public float GetXPosition { get { return rt.anchoredPosition.x; } }
    public float GetWidth { get { return rt.rect.width; } }

    public void Initialize(float tickerWidth, float pixelsPerSecond, string msg, string txtDivider, Canvas canvasUsed)
    {
        this.tickerWidth = tickerWidth;
        this.pixelsPerSecond = pixelsPerSecond;
        rt = GetComponent<RectTransform>();
        this.canvasUsed = canvasUsed;
        GetComponent<Text>().text = msg + txtDivider;
    }

    // Update is called once per frame
    void Update()
    {
        rt.position += (Vector3.left * pixelsPerSecond * Time.deltaTime) / canvasUsed.GetComponent<RectTransform>().localPosition.z;
        if (GetXPosition <= 0 - tickerWidth - GetWidth)
        {
            Destroy(gameObject);
        }
    }
}
