using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    // InputActions
    // Movement
    /* Vector2 movInput;
    // Debug
    Key debugQuitKey; */
    [Tooltip("Cam Movement Multiplier")]
    public float camMovMult = 0.1f;
    [Tooltip("Cam Zoom Multiplier")]
    public float camZmMult = 0.1f;
    [Tooltip("Cam Rotate Multiplier")]
    public float camRtMult = 6f;
    [Header("Gradient Ramp for Camera Background")]
    public Gradient statGradient;
    public Color camColor = new Color(0.5f, 0.5f, 0.5f);

    public enum Faction { MTF, DClass, Researcher, Sec, CI, SCP, GOC, Civ, Misc, VIP, Special };
    [Header("Player Controls")]
    public Faction currentFaction;

    public GameObject barrelPrefab;
    public GameObject barrelAnnounce;
    public Transform barrelAnnounceCanvas;
    public GameObject token;
    public Transform spawnPoint;
    public float spawnPointSpread = 1f;

    // private float msOldPos = 0f;
    private Quaternion camRotOriginal = Quaternion.Euler(90, 0, 0);
    private Quaternion camRotTarget = Quaternion.Euler(90, 0, 0);
    // private float camRotSlerpTime = 0.0f;
    private Camera cam;
    private AudioSource audioSource;

    private int BARRELS = 0;

    void Awake()
    {
        if (spawnPoint == null)
        {
            spawnPoint = GameObject.Find("FoundationSpawn").GetComponent<Transform>(); // TODO: Replace with dynamic spawn point
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    void Move(Vector2 dir)
    {
        Debug.Log("h: " + dir);
    }

    // Update is called once per frame
    void Update()
    {/*

        // Color Control
        cam.backgroundColor = camColor;

        // Numpad 5 to Kill the game immediately (Only for Debug)
        if (kb.numpad5Key.wasPressedThisFrame)
        {
            Debug.Log("Die");
            ExitGame();
        }
        */

        if (GameObject.Find("ControlGauge/ControlBar").GetComponent<RectTransform>().localPosition.y <= -30 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) // TODO: Remove!
        {
            audioSource.volume *= 0.9f;
        }
    }

    public void BARRELSPAWN() // Spawn a barrel
    {
        Instantiate(barrelPrefab, transform.position, Quaternion.identity);
        BARRELS++;
        if (BARRELS > 25)
        {
            GameObject hhhh = Instantiate(barrelAnnounce, transform.position, Quaternion.identity);
            BARRELS = 0;
            hhhh.transform.SetParent(barrelAnnounceCanvas, false);
        }
    }

    public void spawnBasicToken()
    {
        Instantiate(token, new Vector3(spawnPoint.transform.position.x + Random.Range(-spawnPointSpread, spawnPointSpread), spawnPoint.transform.position.y, spawnPoint.transform.position.z + Random.Range(-spawnPointSpread, spawnPointSpread)), Quaternion.identity);
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        // Application.Quit();
    }
}
