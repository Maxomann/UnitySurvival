using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    private Vector3 startPosition;

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

    public float HUNGER_GAIN_PER_TIME;
    public float THIRST_GAIN_PER_TIME;

    float health;
    float hunger;
    float thirst;
    float tiredness;
    float temperature;

    bool alive;

    private void init_values()
    {
        GetComponent<Transform>().position = startPosition;

        health = HEALTH_MAX;
        hunger = HUNGER_MIN;
        thirst = THIRST_MIN;
        tiredness = TIREDNESS_MIN;
        temperature = TEMPERATURE_NORMAL;

        alive = true;
    }

    // Use this for initialization
    void Start()
    {
        startPosition = GetComponent<Transform>().position;
        init_values();
    }

    // Update is called once per frame
    void Update()
    {
        hunger += (float)HUNGER_GAIN_PER_TIME * Time.deltaTime;
        thirst += (float)THIRST_GAIN_PER_TIME * Time.deltaTime;

        checkValueLimits();
    }

    void checkValueLimits()
    {
        if (hunger > HUNGER_MAX)
            die();
        if (thirst > THIRST_MAX)
            die();

        if (hunger < HUNGER_MIN)
            hunger = HUNGER_MIN;
        if (thirst < THIRST_MIN)
            thirst = THIRST_MIN;
    }

    private void die()
    {
        alive = false;
    }

    public void respawn()
    {
        init_values();
    }

    public void eat(ref FoodObject food)
    {
        hunger -= food.HUNGER_VALUE;
        thirst -= food.THIRST_VALUE;

        checkValueLimits();

        Destroy(food);

        Debug.Log("Eat");
        Debug.Log(hunger);
        Debug.Log(thirst);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        FoodObject obj = coll.gameObject.GetComponent<FoodObject>();
        if (obj != null)
            eat(ref obj);
    }
}
