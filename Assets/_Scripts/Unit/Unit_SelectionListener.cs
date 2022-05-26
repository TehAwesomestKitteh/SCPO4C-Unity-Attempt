using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit_SelectionListener : MonoBehaviour
{
    public Unit_Core parent;
    public SpriteRenderer parentSpriteRenderer;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.GetComponent<Unit_Core>();
        parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.isSelected)
        {
            spriteRenderer.color = Color.green;
            spriteRenderer.sprite = parentSpriteRenderer.sprite;
        } else
        {
            spriteRenderer.sprite = null;
        }
    }
}
