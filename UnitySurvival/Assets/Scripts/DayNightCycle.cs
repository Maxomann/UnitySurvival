using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour
{

    //Global Variables
    public static DayNightCycle curController { get; private set; } // Public getters, private setters
    public static int curLightMode { get; private set; }
    public static int totalDayLength { get; private set; }
    public static int[] timeList { get; private set; }
    public static Color[] colorList { get; private set; }
    public static float transitionSpeed { get; private set; }
    public static float transitionSpeedDelay { get; private set; }

    //Variables for the Editor Inspector window
    public float _transitionSpeed = 0.01f;                                                                      // How fast to move through the Linear interpolation (Lerp)
    public float _transitionSpeedDelay = 0.05f;                                                                 // How many seconds to wait before Lerping further
    public int _totalDayLength = 40;                                                                            // How many seconds are in the whole cycle
    public int[] _timeList = new int[] { 1, 2, 3, 4 };                                                          // When to trigger the color switches
    public Color[] _colorList = new Color[] { Color.red, Color.cyan, Color.magenta, Color.yellow, Color.blue }; // What color to become
                                                                                                                // Since the first color is a default color, the color list has to be one size larger than the timelist
                                                                                                                

    void Start()
    {
        if (DayNightCycle.curController == null)
        { // Another script hasn't already activated the ambient light system
            if (_timeList != null && _colorList != null && _totalDayLength > 0)
            { // The game object is now properly set up
                if (_colorList.Length != _timeList.Length + 1)
                {
                    Debug.LogError("Color list size must be one more than time list size! The first color is the default color, the first time is when to move to the second color!");
                    return;
                }
                DayNightCycle.curController = this; // From here on the ambient lighting system is controlled
                DayNightCycle.totalDayLength = _totalDayLength;
                DayNightCycle.timeList = _timeList;
                DayNightCycle.colorList = _colorList;
                DayNightCycle.transitionSpeed = _transitionSpeed;
                DayNightCycle.transitionSpeedDelay = _transitionSpeedDelay;
                RenderSettings.ambientLight = _colorList[0]; // Set the default color
            }
        }
    }

    float lastTime; // The time of day on the last FixedUpdate
    bool waitingForDay; // Are we waiting for the next day?
    bool needsClerp; // Should we restart the process?
    void FixedUpdate()
    {
        if (DayNightCycle.curController == this)
        {
            float curTime = Time.fixedTime % DayNightCycle.totalDayLength; // The current time of day is the number of seconds since start mod the total day length in seconds
            if (lastTime > curTime)
            { // The day has reset
                DayNightCycle.curLightMode = 0;
                needsClerp = true;
                waitingForDay = false;
            }
            if (!waitingForDay)
            { // We are still within the time list's bounds
                if (curTime > DayNightCycle.timeList[DayNightCycle.curLightMode])
                { // The current time of day is more than the switch event
                    DayNightCycle.curLightMode++; // So switch to the next light mode
                    needsClerp = true; // Then start the process
                    if (DayNightCycle.curLightMode >= DayNightCycle.timeList.Length)
                    { // We're going too far outside the time list
                        waitingForDay = true; // Wait until the next day resets the counter
                    }
                }
            }
            if (needsClerp)
            { // Something changed our color
                needsClerp = false; // Do not restart the process again
                StopCoroutine("CLerp"); // Stop the current transition
                StartCoroutine("CLerp", colorList[curLightMode]); // Lerp to this now
            }
            lastTime = curTime; // For checking if the day has reset
        }
    }

    IEnumerator CLerp(Color newColor)
    {
       
        Color oldColor = RenderSettings.ambientLight; // The current color
        for (float t = 0; t < 1 + DayNightCycle.transitionSpeed; t += DayNightCycle.transitionSpeed)
        { // We need to add one extra step
            RenderSettings.ambientLight = Color.Lerp(oldColor, newColor, t); // Lerp the ambient light to the new color
            yield return new WaitForSeconds(DayNightCycle.transitionSpeedDelay); // Wait for the assigned amount of seconds
        }
       
    }
}