using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    private Vector3 startPosition;

    public uint HUNGER_MAX;
    public uint HUNGER_MIN;
    public uint THIRST_MAX;
    public uint THIRST_MIN;
    public uint TIREDNESS_MAX;
    public uint TIREDNESS_MIN;

    public float HUNGER_GAIN_PER_TIME;
    public float THIRST_GAIN_PER_TIME;
    public float TIREDNESS_GAIN_PER_TIME;

    float hunger;
    float thirst;
    float tiredness;

    bool alive;

    private void init_values()
    {
        GetComponent<Transform>().position = startPosition;

        hunger = HUNGER_MIN;
        thirst = THIRST_MIN;
        tiredness = TIREDNESS_MIN;

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
        tiredness += (float)TIREDNESS_GAIN_PER_TIME * Time.deltaTime;

        checkValueLimits();
    }

    void checkValueLimits()
    {
        if (hunger > HUNGER_MAX)
            die();
        if (thirst > THIRST_MAX)
            die();
        if (tiredness > TIREDNESS_MAX)
            die();

        if (hunger < HUNGER_MIN)
            hunger = HUNGER_MIN;
        if (thirst < THIRST_MIN)
            thirst = THIRST_MIN;
        if (tiredness < TIREDNESS_MIN)
            tiredness = TIREDNESS_MIN;
    }

    private void die()
    {
        alive = false;

        Debug.Log("!DEAD!");
        Debug.Log(hunger);
        Debug.Log(thirst);
        Debug.Log(tiredness);
    }

    public void respawn()
    {
        init_values();

    }
    public void eat(GameObject obj, FoodObject food)
    {
        food.onEat();
        hunger -= food.HUNGER_VALUE;
        thirst -= food.THIRST_VALUE;

        checkValueLimits();

        Destroy(obj);

        Debug.Log("Eat");
        Debug.Log(hunger);
        Debug.Log(thirst);
    }

    public void sleep(GameObject obj, SleepObject tent)
    {
        tiredness -= tent.TIREDNESS_VALUE;

        checkValueLimits();

        Debug.Log("Sleep");
        Debug.Log(tiredness);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject obj = coll.gameObject;
        FoodObject food = obj.GetComponent<FoodObject>();
        SleepObject tent = obj.GetComponent<SleepObject>();
        if (food != null)
            eat(obj, food);
        if (tent != null)
            sleep(obj, tent);
    }

    public float getHunger()
    {
        float tempHunger;
        tempHunger = hunger;
        return tempHunger;
    }

    public float getThirst()
    {
        float tempThirst;
        tempThirst = thirst;
        return tempThirst;
    }

    public float getTiredness()
    {
        float tempTiredness;
        tempTiredness = tiredness;
        return tempTiredness;
    }
}
