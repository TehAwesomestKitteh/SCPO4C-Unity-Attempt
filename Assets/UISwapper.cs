using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwapper : MonoBehaviour
{
    public Sprite[] spriteList;
    public Sprite disabledSprite;
    public bool disabled = false;
    private int spriteIteration = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSprite()
    {
        if (!disabled)
        {
            spriteIteration++;
            if (spriteIteration >= spriteList.Length)
            {
                spriteIteration = 0;
            }
            GetComponent<Image>().sprite = spriteList[spriteIteration];
        } else
        {
            GetComponent<Image>().sprite = disabledSprite;
        }
    }
}
