using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour
{
    // Stuffs
    [Header("Stuffs")]
    public RectTransform selectionBox;
    public GameObject notifPrefab;

    // Events
    public bool dragSelectLoss = false;

    // Private Vars
    Vector3 startPosition;

    private void Awake()
    {
        if (selectionBox == null)
        {
            selectionBox = GameObject.Find("MainCam/Canvas/SelectionBox").GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        // BOX DRAG SELECTION
        if (Input.GetMouseButtonDown(0) && !dragSelectLoss)
        {
            startPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && !dragSelectLoss)
        {
            updateSelectionBox(Input.mousePosition);
        }
    }

    void updateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
        {
            selectionBox.gameObject.SetActive(true);
        }

        float width = curMousePos.x - startPosition.x;
        float height = curMousePos.y - startPosition.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPosition;
    } // TODO: me

    void notifPopup(string msg, float delay, int type)
    {
        switch (type)
        {
            case 0: // GENERIC
            default:

                break;
            case 1: // WARN
                break;
            case 2: // DANGER
                break;
        }
    }
}
