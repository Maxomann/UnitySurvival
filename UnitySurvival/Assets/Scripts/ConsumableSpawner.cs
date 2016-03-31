using UnityEngine;
using System.Collections;

public class ConsumableSpawner : MonoBehaviour
{
    bool timerActive = false;
    int timePassed = 0;//in milliseconds
    public int TIME_INTERVALL_CHECK_FOR_SPAWN;//in milliseconds

    GameObject spawnedObject = null;
    Transform thisTransform;

    public int SPAWN_IN_ONE_OF_HOW_MANY;//spawn in one of x checks

    void spawnObject()
    {
        int randomNumber = Random.Range(0, 3);

        GameObject obj = null;

        switch (randomNumber)
        {
            case 0:
                obj = (GameObject)Instantiate(Resources.Load("Food/PrefabCoconut"));
                break;
            case 1:
                obj = (GameObject)Instantiate(Resources.Load("Food/PrefabPineapple"));
                break;
            case 2:
                obj = (GameObject)Instantiate(Resources.Load("Food/PrefabWatermelon"));
                break;
            default:
                Debug.Log("Switch randomNumber in spawnObject() error");
                break;
        }

        obj.GetComponent<Transform>().position = thisTransform.position;
        spawnedObject = obj;
    }

    // Use this for initialization
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject == null)
        {
            if (timerActive)
            {
                if (timePassed > TIME_INTERVALL_CHECK_FOR_SPAWN)
                {
                    int shouldSpawn = Random.Range(0, SPAWN_IN_ONE_OF_HOW_MANY);

                    if (shouldSpawn == 0)
                    {
                        spawnObject();
                    }
                    timerActive = false;
                    timePassed = 0;
                }
                else
                {
                    timePassed += (int)(Time.deltaTime * 1000.0);
                }
            }
            else
            {
                timerActive = true;
            }
        }
    }
}
