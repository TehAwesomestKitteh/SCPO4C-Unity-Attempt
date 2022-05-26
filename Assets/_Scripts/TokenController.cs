using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TokenController : MonoBehaviour
{
    // PERSONNELL DETAILS OF TOKEN
    public enum Faction { MTF, DClass, Researcher, Sec, CI, SCP, GOC, Civ, Misc, VIP, Special };
    public enum Status { OK, HURT, DOWN, CRIT, MIA, KIA };
    [Header("Personnel Settings")]
    public Faction currentFaction;
    public Status status;

    // UI/VALUE DETAILS OF TOKEN
    [Header("UI/Value Settings")]
    [Range(0.0f, 100.0f)]
    public float health = 100f;
    [Range(0.0f, 100.0f)]
    public float ammo1;
    [Range(0.0f, 100.0f)]
    public float ammo2;

    // FUNCTIONAL VALUES OF TOKEN
    [Header("Functional Settings")]
    public bool Selected = false;
    [Range(0.0f, 359.99f)]
    public float fov = 90;
    public float viewDist = 5;
    public float movSpd = 1;


    // Unity Stuff
    private Renderer rend;
    private Material colorMat;
    NavMeshAgent agent;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        colorMat = GetComponent<Renderer>().material;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Token Selection Col Check
        switch (status)
        {
            case Status.OK:
            default:
                colorMat.SetColor("SelColor", new Color(0f, 1f, 0f));
                break;
            case Status.HURT:
                colorMat.SetColor("SelColor", new Color(1f, 1f, 0f));
                break;
            case Status.DOWN:
                colorMat.SetColor("SelColor", new Color(1f, 0.5f, 0f));
                break;
            case Status.CRIT:
                colorMat.SetColor("SelColor", new Color(1f, 0f, 0f));
                break;
            case Status.MIA:
                colorMat.SetColor("SelColor", new Color(1f, 1f, 0f));
                break;
            case Status.KIA:
                colorMat.SetColor("SelColor", new Color(0.5f, 0f, 0f));
                break;
        }

        // Token Selection - Highlight
        if (Selected)
        {
            colorMat.SetInt("Vector1_92751517", 1);
        } else
        {
            colorMat.SetInt("Vector1_92751517", 0);
        }

        // Token Selection
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            bool foundToken = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform == transform)
                {
                    Selected = true;
                    foundToken = true;
                    // Debug.DrawLine(Camera.main.GetComponent<Transform>().transform.position, hit.point, Color.red, 8); >>> TRACK SELF CLICK
                    break;
                }
            }

            if (!foundToken && !Input.GetKey(KeyCode.LeftShift))
            {
                Selected = false;
            }
            
        }

        // Token Movement
        if (Input.GetMouseButtonDown(1) && Selected && (status != Status.KIA))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "MovableTerrain")
                {
                    agent.SetDestination(new Vector3(hit.point.x, transform.position.y, hit.point.z));
                    /* TELEPORTATION TEST
                    transform.position = new Vector3(hit.point.x, 0.9f, hit.point.z); */
                }
            }
        }

        if ((transform.position.x > agent.destination.x - 0.5 && transform.position.x < agent.destination.x + 0.5) &&
            (transform.position.z > agent.destination.z - 0.5 && transform.position.z < agent.destination.z + 0.5))
        {
            agent.ResetPath();
        }
    }

    // Token Mouse Hover
    private void OnMouseEnter()
    {
        colorMat.SetInt("isHoverRef", 1);
    }

    private void OnMouseExit()
    {
        colorMat.SetInt("isHoverRef", 0);
    }

    // Token Damage
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "KILLREGION" && status != Status.KIA) // If this is a KILLREGION and token alive. The token is DEAD
        {
            status = Status.KIA;
            rb.constraints = RigidbodyConstraints.None;
            rb.AddTorque(Random.Range(0f, 20f), Random.Range(0f, 20f), Random.Range(0f, 20f));
            rb.AddForce(Random.Range(0f, 10f), Random.Range(0f, 5f), Random.Range(0f, 10f));
            Vector3 ctrlPos = GameObject.Find("ControlGauge/ControlBar").GetComponent<RectTransform>().localPosition;
            ctrlPos.Set(ctrlPos.x, ctrlPos.y - 10, ctrlPos.z); // TODO: Change "10" to dynamic score/value
            GameObject.Find("ControlGauge/ControlBar").GetComponent<RectTransform>().localPosition = ctrlPos;
            agent.enabled = false;
            rb.useGravity = true;
        }
    }
}