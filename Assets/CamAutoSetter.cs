using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAutoSetter : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
