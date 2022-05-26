using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalBarAnimScript : MonoBehaviour
{
    public float yKillLine;
    public float ySpawnLine;
    public bool spawned = false;
    public float ySpeed;
    public float overTime = 2f;

    [SerializeField]
    float locPosY;

    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        locPosY = rect.localPosition.y;
        rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y + ySpeed * (Time.deltaTime * overTime), rect.localPosition.z);
        if (rect.localPosition.y >= ySpawnLine && !spawned)
        {
            spawned = true;
            GameObject newSpawn = Instantiate(gameObject, rect.position, rect.rotation, gameObject.transform.parent);
            DiagonalBarAnimScript scrpt = newSpawn.GetComponent<DiagonalBarAnimScript>();
            scrpt.spawned = false;
            newSpawn.transform.localPosition = new Vector3(newSpawn.transform.localPosition.x, newSpawn.transform.localPosition.y - rect.localScale.x, newSpawn.transform.localPosition.z);
        }

        if (rect.localPosition.y >= yKillLine)
        {
            Destroy(gameObject);
        }
    }
}
