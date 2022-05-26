using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterTextGenerator : MonoBehaviour
{
    public string[] txtList;
    public bool isAnnouncement;
    [Range(0.5f, 20f)]
    public float textPerSecond = 5f;
    [Range(0f, 5f)]
    public float timeBeforeNextIndex;

    int currTxtIndex = 0;
    float timePassed = 0;
    float txtProgress = 0f;
    int txtCurrent = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Text>().text.Length < txtList[currTxtIndex].Length)
        {
            txtProgress += textPerSecond * Time.deltaTime;
            while (txtCurrent < Mathf.Floor(txtProgress))
            {
                GetComponent<Text>().text += txtList[currTxtIndex][txtCurrent];
                txtCurrent++;
            }
        } else
        {
            timePassed += Time.deltaTime;
            if (timePassed >= timeBeforeNextIndex)
            {
                if (isAnnouncement)
                {
                    currTxtIndex++;
                    if (currTxtIndex >= txtList.Length)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        GetComponent<Text>().text = "";
                        timePassed = 0;
                        txtCurrent = 0;
                        txtProgress = 0f;
                    }
                }
                else
                {
                    currTxtIndex = Random.Range(0, txtList.Length);
                    GetComponent<Text>().text = "";
                    timePassed = 0;
                    txtCurrent = 0;
                    txtProgress = 0f;
                }                
            }
        }
    }
}
