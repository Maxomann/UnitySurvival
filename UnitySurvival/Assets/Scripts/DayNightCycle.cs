using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{

    [Header("Time Settings")]
    public int dayNightLength = 240; //Total length of a Cycle in seconds
    public float curTime = 0;
    [Range(0, 1)]
    public float duskTime = 0.75f; //After how much % of a Cycle dusk begins
    [Range(0, 1)]
    public float dawnTime = 0.25f; //After how much % of a Cycle dawn begins
    public float transitionDurationSeconds = 10;

    [Header("Camera Settings")]
    public Camera cam;
    public Color lightColor;
    public Color darkColour;


    private void Start()
    {

        if (curTime / dayNightLength > dawnTime && curTime / dayNightLength < duskTime)
        {

            Debug.Log("It is day");

        }
        else {

            Debug.Log("It is night");
        }
    }

    private void Update()
    {

        InvokeRepeating("DayNightTick", 1f, 1f);
    }

    private void DayNightTick()
    {

        if (curTime++ >= dayNightLength)
        {

            curTime = 0;
        }

    }

}


