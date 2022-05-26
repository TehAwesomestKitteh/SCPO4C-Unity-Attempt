using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core_GameManager : MonoBehaviour
{
    [Header("Cursor Controls")]
    public Texture2D clickingMouseTex;
    public bool isCursorVisible = false;
    public enum CURSORSTATE {NORMAL, CLICKING};
    public CURSORSTATE currentCursorState = CURSORSTATE.NORMAL;

    [Header("Player Controls")]
    [Space(15)]
    public Transform camObject;
    public Cinemachine.CinemachineVirtualCamera cam;
    public float camLowerLimit = 1f;
    public float camUpperLimit = 20f;
    public float camMoveSpeed = 5f;

    [Header("Gameplay Variables")]
    public bool isPaused;

    // Cash
    [Space(15)]
    public Text cashDisplay;
    public float cash = 0;
    public float cashRate = 100;
    private float cashTimer = 0f;

    // Time
    [Space(15)]
    public Text timeDisplay;
    public float secFragValue = 0f;
    public int secValue = 0;
    public int minValue = 0;
    public int hrValue = 0;
    public int dayValue = 0;
    public float timeRate = 3;

    // Faction
    /* MTF - Mobile Task Force, O4
     * FDN - Foundation/Site Personnel
     * DCS - D-Class, Test Subjects
     * CI - Chaos Insurgency, Generic Raiders, Generic Military
     * SCP - SCPs
     * GOC - Global Occult Coalition, Generic Law Enforcement [TBA]
     * CIV - Civilian, VIPs, Objective Fodder [TBA]
     * MCD - Marshall Carter and Dark, Generic Mercenaries [TBA]
     * CBG - Church of The Broken God, UI Disruption [TBA]
     */
    // public enum PLAYERFACTION { MTF, FDN, DCS, CI, GOC, SCP, CIV, MCD, CBG };
    // >> ABOVE TEMPORARILY COMMENTED/REPLACED BECAUSE I AM AN OVERSHOOTING IDIOT <<
    public enum PLAYERFACTION { P1, P2, P3, P4, P5, P6, P7, P8, NEUTRAL, HOSTILE, ALLY }
    [Space(15)]
    // public PLAYERFACTION currentFaction = PLAYERFACTION.MTF;
    public PLAYERFACTION currentFaction = PLAYERFACTION.NEUTRAL;

    // Start is called before the first frame update
    void Start()
    {
        hrValue = System.DateTime.Now.Hour;
        minValue = System.DateTime.Now.Minute;
        secValue = System.DateTime.Now.Second;
    }

    // Update is called once per frame
    void Update()
    {
        #region Cursor State Check Loop
        // Cursor State Check Loop
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            currentCursorState = CURSORSTATE.CLICKING;
        } else
        {
            currentCursorState = CURSORSTATE.NORMAL;
        }
        #endregion

        #region Cursor State Set
        switch (currentCursorState)
        {
            case CURSORSTATE.CLICKING:
                Cursor.SetCursor(clickingMouseTex, new Vector2(0, 0), CursorMode.Auto);
                break;
            case CURSORSTATE.NORMAL:
            default:
                Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
                break;
        }

        Cursor.visible = isCursorVisible;
        #endregion

        #region UI Updates
        cashDisplay.text = "OPERATION FUNDS: $" + cash;
        #endregion

        // Cash increase
        cashTimer += Time.deltaTime;
        if (cashTimer >= 0.5f)
        {
            cashTimer -= 0.5f;
            cash += cashRate * 0.5f;
            cash = Mathf.Floor(cash);
        }

        // Time Setting (Yes this is super inefficient and broken but my brain can't grasp a more efficient code)
        secFragValue += Time.deltaTime * timeRate; // Convert Delta time into second fragments
        if (secFragValue > 1f) // If frag value is more than a second, add second values til it no longer is
        {
            secValue += (int)Mathf.Floor(secFragValue);
            secFragValue -= Mathf.Floor(secFragValue);
        }
        if (secValue >= 60) // If second value is more than a minute, add minute values til it no longer is
        {
            minValue += secValue / 60;
            secValue %= 60;
        }
        while (minValue >= 60) // Repeat process
        {
            hrValue += minValue / 60;
            minValue %= 60;
        }
        while (hrValue >= 24)
        {
            dayValue += hrValue / 24;
            hrValue %= 24;
        }
        if (hrValue == 0) // Manual clock conventions string output (12 is the start, 11 is the end)
        {
            timeDisplay.text = "DAY " + dayValue + " - 12:" + String.Format("{0:00}", minValue) + ":" + String.Format("{0:00}", secValue) + " AM";
        } else if (hrValue >= 1 && hrValue <= 11)
        {
            timeDisplay.text = "DAY " + dayValue + " - " + String.Format("{0:00}", hrValue) + ":" + String.Format("{0:00}", minValue) + ":" + String.Format("{0:00}", secValue) + " AM";
        } else if (hrValue == 12)
        {
            timeDisplay.text = "DAY " + dayValue + " - 12:" + String.Format("{0:00}", minValue) + ":" + String.Format("{0:00}", secValue) + " PM";
        } else
        {
            timeDisplay.text = "DAY " + dayValue + " - " + String.Format("{0:00}", hrValue - 12) + ":" + String.Format("{0:00}", minValue) + ":" + String.Format("{0:00}", secValue) + " PM";
        }

        

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            cam.m_Lens.OrthographicSize -= Input.GetAxis("Mouse ScrollWheel");
            cam.m_Lens.OrthographicSize = Mathf.Clamp(cam.m_Lens.OrthographicSize, camLowerLimit, camUpperLimit);
        }

        if (Input.GetAxis("Horizontal") != 0f)
        {
            camObject.Translate(new Vector2(Input.GetAxis("Horizontal") * camMoveSpeed, 0));
        }

        if (Input.GetAxis("Vertical") != 0f)
        {
            camObject.Translate(new Vector2(0, Input.GetAxis("Vertical") * camMoveSpeed));
        }



        
    }
}
