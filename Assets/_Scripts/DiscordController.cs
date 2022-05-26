using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    public ActivityManager activityManager;
    public bool forceDiscUpdate = false;
    public string detailsDiscord = "SCP-173 is behind you";
    public string stateDiscord = "Now Experimenting...";
    public string ImgDiscord = "o4cart_default";
    public string TxtDiscord = "O4C Main Menu";
    public string subImgDiscord = "";
    public string subTxtDiscord = "";
    public long gameStartEpoch = 0;

    // Use this for initialization
    void Start()
    {
        discord = new Discord.Discord(699598631031930900, (System.UInt64)Discord.CreateFlags.Default);
        activityManager = discord.GetActivityManager();
        var activity = LoadActivity();
        
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Discord Integration OK!");
            }
        });

        // Scene Load Listener
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Restrict Controller to only have 1 existing
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ForceDiscUpdate();
    }

    // Activity Setter
    private Discord.Activity LoadActivity()
    {
        if (SceneManager.GetActiveScene().name.StartsWith("STAGE_"))
        {
            detailsDiscord = "Lockdown: " + SceneManager.GetActiveScene().name.Substring(6);
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            gameStartEpoch = (long)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        }
        var activity = new Discord.Activity
        {
            Details = detailsDiscord,
            State = stateDiscord,
            Assets = {
                LargeImage = ImgDiscord,
                LargeText = TxtDiscord,
                SmallImage = subImgDiscord,
                SmallText = subTxtDiscord
            },
            Timestamps =
            {
                Start = gameStartEpoch
            }
        };
        return activity;
    }

    // Update is called once per frame
    void Update()
    {
        if (forceDiscUpdate)
        {
            ForceDiscUpdate();
        }
        discord.RunCallbacks();
    }

    // Force Discord Rich Presence Reload
    private void ForceDiscUpdate()
    {
        var activity = LoadActivity();
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Discord Integration Refreshed!");
            }
        });
        forceDiscUpdate = false;
    }

    // Shutdown Controller
    private void OnApplicationQuit()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        activityManager.ClearActivity((result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Debug.Log("Activity Clear Success!");
            }
            else
            {
                Debug.Log("Activity Clear Failed");
            }
        });
        discord.Dispose();
        Debug.Log("Quit app");
    }
}