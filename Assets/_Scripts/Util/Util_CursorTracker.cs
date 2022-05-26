using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_CursorTracker : MonoBehaviour
{
    public enum TRACKERMODE {XY_Absolute, XY_Fixed, Raycast};
    public TRACKERMODE trackerMode = TRACKERMODE.XY_Absolute;
    public Vector3 aaaaa;
    public Vector3 aaa;

    // Update is called once per frame
    void Update()
    {
        switch (trackerMode)
        {
            case TRACKERMODE.Raycast:
                // TODO: Raycast
                break;
            case TRACKERMODE.XY_Fixed:
                // If cursor not on screen, cancel
                Vector3 cursorChecker = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                if (cursorChecker.x < 0 || cursorChecker.x > 1 || cursorChecker.y < 0 || cursorChecker.y > 1) break;

                // Else, begin calculation
                Vector3 fixedMousePos = Input.mousePosition;
                fixedMousePos.x = Mathf.Clamp(fixedMousePos.x, 0, Screen.width);
                fixedMousePos.y = Mathf.Clamp(fixedMousePos.y, 0, Screen.height);
                aaa = fixedMousePos;
                Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                camPos.z = 0;
                aaaaa = camPos;
                gameObject.transform.position = camPos;
                break;
            case TRACKERMODE.XY_Absolute:
            default:
                // Absolute XY Tracking (100 pixels is 100 on X)
                gameObject.transform.position = Input.mousePosition;
                break;
        }
    }
}
