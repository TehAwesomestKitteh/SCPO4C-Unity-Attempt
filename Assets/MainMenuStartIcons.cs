using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class MainMenuStartIcons : MonoBehaviour
    , IPointerClickHandler
    , IDragHandler
    , IBeginDragHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{

    public Color32 panelCol = new Color32(255, 255, 255, 100);
    public Color32 panelColSel = new Color32(128, 128, 128, 200);
    public Color32 panelColUnSel = new Color32(255, 255, 255, 10);
    [Range(0, 255)]
    public int panelColAlpha = 100;
    public bool Selected = false;
    public enum Func {START, SETTINGS, CONTROLS, TEXT_STATIC, EXIT};
    public Func func;

    [Header("STATIC WINDOW CONTROLS")]
    public GameObject windowObject;
    public string headerText;
    [TextArea(3, 5)]
    public string bodyText;

    private Color32 currColor;
    private Transform highlightPanel;
    private float clickTime = 0f;

    private Vector2 lastMousePosition;
    private RectTransform parentCanvasRect;
    private RectTransform panelRect;
    private Vector3 pos;

    private void Start()
    {
        highlightPanel = transform;
        parentCanvasRect = GetComponentInParent<RectTransform>();
        panelRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        /*Vector3 minPos = parentCanvasRect.rect.min - panelRect.rect.min;
        Vector3 maxPos = parentCanvasRect.rect.max - panelRect.rect.max;

        pos.x = Mathf.Clamp(panelRect.localPosition.x, minPos.x, maxPos.x);
        pos.y = Mathf.Clamp(panelRect.localPosition.y, minPos.y, maxPos.y);

        Debug.Log(parentCanvasRect.rect.min.ToString() + " " + parentCanvasRect.rect.max.ToString());

        panelRect.localPosition = pos;*/

        panelRect.localPosition = new Vector3(Mathf.Clamp(panelRect.localPosition.x, -1920 / 2, 1920 / 2), Mathf.Clamp(panelRect.localPosition.y, -1080 / 2, 1080 / 2)); // 1920 and 1080 to change based on canvas dimensions on cam
        highlightPanel.GetComponent<Image>().color = currColor;
    }

    public void OnMouseEnter()
    {
        Debug.Log("ENTERD NEW");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Selected && ((eventData.clickTime - clickTime) < 0.25f))
        {
            IconFunc();
        }
        currColor = panelColSel;
        Selected = true;
        clickTime = eventData.clickTime;
    }

    public void IconFunc()
    {
        switch (func)
        {
            case Func.START:
                SceneManager.LoadScene("STAGE_TESTZONE", LoadSceneMode.Single);
                break;
            case Func.SETTINGS:
                break;
            case Func.CONTROLS:
                break;
            case Func.TEXT_STATIC:
                GameObject newWindow = Object.Instantiate(windowObject, transform.parent) as GameObject;
                newWindow.transform.localScale.Set(1f, 1f, 1f);
                newWindow.transform.Find("HeaderText").GetComponent<Text>().text = headerText;
                newWindow.transform.Find("ScrollView").Find("Viewport").Find("Content").Find("BodyText").GetComponent<Text>().text = bodyText;
                // transform.Find("InfoPanel").gameObject.SetActive(true);
                break;
            case Func.EXIT:
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
                break;
            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENTERD");
        if (!Selected)
        {
            currColor = panelCol;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Selected = true;
        currColor = panelColSel;
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        RectTransform rect = GetComponent<RectTransform>();

        Vector3 newPosition = rect.position + new Vector3(diff.x, diff.y, 0);
        Vector3 oldPos = rect.position;

        rect.position = newPosition;
        /*if (!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }*/
        lastMousePosition = currentMousePosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (Selected)
        {
            Selected = false;
        }
        currColor = panelColUnSel;
    }
}
