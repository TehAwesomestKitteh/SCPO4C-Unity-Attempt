using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_NavTarget : MonoBehaviour
{
    public Unit_Core parent;

    void Start()
    {
        parent = gameObject.transform.parent.GetComponent<Unit_Core>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && parent.isSelected)
        {
            Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = camPos;
        }
    }
}
