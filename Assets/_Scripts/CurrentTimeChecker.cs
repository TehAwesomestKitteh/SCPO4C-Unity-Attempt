using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CurrentTimeChecker : MonoBehaviour
{
    public enum DateFormat { DMY, MDY, YMD, YDM, UTC, RAW, HEX }
    public DateFormat currentDateFormat;

    // Update is called once per frame
    void Update()
    {
        switch (currentDateFormat)
        {
            case DateFormat.DMY: // Day/Mon/Year; default
            default:
                if ((System.DateTime.Now.Second % 2) == 0)
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
                }
                else
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH mm ss - dd/MM/yyyy");
                }
                break;
            case DateFormat.MDY: // Mon/Day/Year
                if ((System.DateTime.Now.Second % 2) == 0)
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH:mm:ss - MM/dd/yyyy");
                }
                else
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH mm ss - MM/dd/yyyy");
                }
                break;
            case DateFormat.YMD: // Year/Mon/Day
                if ((System.DateTime.Now.Second % 2) == 0)
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH:mm:ss - yyyy/MM/dd");
                }
                else
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH mm ss - yyyy/MM/dd");
                }
                break;
            case DateFormat.YDM: // Year/Day/Mon
                if ((System.DateTime.Now.Second % 2) == 0)
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH:mm:ss - yyyy/dd/MM");
                }
                else
                {
                    GetComponent<Text>().text = System.DateTime.Now.ToString("HH mm ss - yyyy/dd/MM");
                }
                break;
            case DateFormat.UTC:
                GetComponent<Text>().text = System.DateTime.Now.ToString();
                break;
            case DateFormat.RAW:
                GetComponent<Text>().text = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                break;
            case DateFormat.HEX:
                GetComponent<Text>().text = int.Parse(System.DateTime.Now.ToString("yyyyMMdd")).ToString("X") + int.Parse(System.DateTime.Now.ToString("HHmmss")).ToString("X");
                break;
        }
    }
}
