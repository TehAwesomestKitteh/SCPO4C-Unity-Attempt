using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickerScript : MonoBehaviour
{
	public TickerItemScript tickerItemPrefab;
    public Canvas canvasUsed;
	[Range(0.01f, 100f)]
	public float itemDuration = 2.0f;
	public string[] fillerItems;
    [Tooltip("The text used to divide the ticker")]
    public string txtDivider;
	public bool isMovingRight;

	float width;
	float pixelsPerSecond;
    bool doublingFix = false;
	TickerItemScript currentItem;

    // Start is called before the first frame update
    void Start()
    {
		width = GetComponent<RectTransform> ().rect.width;
		pixelsPerSecond = width / itemDuration;
        AddTickerItem(fillerItems[Random.Range(0, fillerItems.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
		if (currentItem.GetXPosition <= -currentItem.GetWidth) {
            if (!doublingFix)
            {
                doublingFix = true;
            } else
            {
                AddTickerItem(fillerItems[Random.Range(0, fillerItems.Length)]);
            }
			
		}
    }

	void AddTickerItem (string msg) {
		currentItem = Instantiate (tickerItemPrefab, transform);
		currentItem.Initialize(width, pixelsPerSecond, msg, txtDivider, canvasUsed);
	}
}
