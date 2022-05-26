using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    float weight;
    float origBcXPos;
    public bool disabled = false;
    public bool open = false;
    public float openRate = 50f;
    public float closeRate = 50f;
    public float currWeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        weight = gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0);
        origBcXPos = gameObject.GetComponent<BoxCollider>().center.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            if (open)
            {
                if (currWeight < 100)
                {
                    currWeight += openRate * Time.deltaTime;
                }
            }
            else
            {
                if (currWeight > 0)
                {
                    currWeight -= closeRate * Time.deltaTime;
                }
            }
            gameObject.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, currWeight);
            gameObject.GetComponent<BoxCollider>().center = new Vector3(0 + (gameObject.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(0) * -0.00022f), 0, 0.01f);
        }
    }

    // Door control
    public void ToggleOpen()
    {
        if (!disabled)
        {
            open = !open;
        }
    }
}
