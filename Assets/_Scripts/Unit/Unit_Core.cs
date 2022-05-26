using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit_Core : MonoBehaviour, IPointerClickHandler
{
    [Header("Unit Variables")]
    public Core_GameManager.PLAYERFACTION unitFaction = Core_GameManager.PLAYERFACTION.NEUTRAL;
    public int unitHP = 80;

    [Space(15)]
    [Header("Function Variables")]
    public bool isSelected = false;
    public bool ignoresRoomSpeed = false;

    [Space(15)]
    [Header("Speed Variables")]
    /*
    public Color rangeColor = Color.magenta;
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float xradius = 5;
    [Range(0, 5)]
    public float yradius = 5;
    [Range(0f, 359f)]
    public float rangeAngle = 0f;
    [Range(0.0f, 359.99f)]
    public float fov = 90;
    public float viewDist = 5;*/
    public float movSpd = 1;
    // LineRenderer line;

    SpriteRenderer spriteRenderer;

    private Transform destTransform;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        destTransform = transform.Find("Destination").transform;
        updateGraphics();

        // line = gameObject.GetComponent<LineRenderer>();
    }

    /* void CreatePoints()
    {
        line.startColor = rangeColor;
        line.endColor = rangeColor;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        float x;
        float y;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * rangeAngle) * viewDist;
            y = Mathf.Cos(Mathf.Deg2Rad * rangeAngle) * viewDist;

            line.SetPosition(i, new Vector3(x, y, 0));
        }
    } */ /* OBSOLETE AI CONTROL */

    // Update is called once per frame
    void Update()
    {
        updateGraphics();
        // CreatePoints();
        
        if (isSelected && Input.GetMouseButtonDown(1))
        {
            destTransform.position = Input.mousePosition;
        }
    }

    // Unit Clicked Handler
    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
    }

    // Change Faction
    public void changeFaction(Core_GameManager.PLAYERFACTION newFaction)
    {
        unitFaction = newFaction;
        updateGraphics();
    }

    // Update Unit Color
    void updateGraphics()
    {
        spriteRenderer.color = Color.grey;
        /* if (!isSpy) // Spy Check + Colors
        {
            switch (unitFaction)
            {
                case Core_GameManager.PLAYERFACTION.MTF: // MTF
                    spriteRenderer.color = new Color(43, 102, 255);
                    break;
                case Core_GameManager.PLAYERFACTION.CI: // Chaos
                    spriteRenderer.color = new Color(0, 128, 0);
                    break;
                case Core_GameManager.PLAYERFACTION.FDN: // Foundation
                    spriteRenderer.color = Color.grey;
                    break;
                case Core_GameManager.PLAYERFACTION.DCS: // D-Class
                    spriteRenderer.color = new Color(255, 128, 0);
                    break;
                case Core_GameManager.PLAYERFACTION.GOC: // GOC
                    spriteRenderer.color = Color.blue;
                    break;
                case Core_GameManager.PLAYERFACTION.MCD: // Marshall Carter Dark
                    spriteRenderer.color = Color.red;
                    break;
                case Core_GameManager.PLAYERFACTION.CIV: // Civilian
                    spriteRenderer.color = Color.yellow;
                    break;
                case Core_GameManager.PLAYERFACTION.CBG: // Church of the Broken God
                    spriteRenderer.color = Color.red;
                    break;
                case Core_GameManager.PLAYERFACTION.SCP: // SCP and Unrecognized faction
                default:
                    spriteRenderer.color = Color.red;
                    break;
            }
        }

        if (!isConcealingWeapon) // Hidden Weapon Check + Sprite
        {
            switch (unitType)
            {
                case UNITTYPE.Ally_Armed:
                    spriteRenderer.sprite = allyArmedSprite;
                    break;
                case UNITTYPE.Enemy_Armed:
                    spriteRenderer.sprite = enemyArmedSprite;
                    break;
                case UNITTYPE.Unarmed:
                    spriteRenderer.sprite = unarmedSprite;
                    break;
                case UNITTYPE.Neutral:
                default:
                    spriteRenderer.sprite = neutralSprite;
                    break;
            }
        }*/
    }
}
