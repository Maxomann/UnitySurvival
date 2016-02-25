using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public uint HEALTH_MAX;
    public uint HEALTH_MIN;
    public uint HUNGER_MAX;
    public uint HUNGER_MIN;
    public uint THIRST_MAX;
    public uint THIRST_MIN;
    public uint TIREDNESS_MAX;
    public uint TIREDNESS_MIN;
    public int TEMPERATURE_MIN;
    public int TEMPERATURE_MAX;
    public int TEMPERATURE_NORMAL;

    uint health;
    uint hunger;
    uint thirst;
    uint tiredness;
    int temperature;

    // Use this for initialization
    void Start()
    {
        health = HEALTH_MAX;
        hunger = HUNGER_MIN;
        thirst = THIRST_MIN;
        tiredness = TIREDNESS_MIN;
        temperature = TEMPERATURE_NORMAL;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
