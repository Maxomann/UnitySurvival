﻿using UnityEngine;
using System.Collections;

public class FoodObject : MonoBehaviour
{

    public int HUNGER_VALUE;
    public int THIRST_VALUE;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onEat()
    {
        Debug.Log("ON_EAT");
        if (GetComponent<AudioSource>() == null)
            Debug.Log("NULL_ERROR");
        if (!GetComponent<AudioSource>().isPlaying)
        {
            Debug.Log("ON_EAT:PLAYING");
            GetComponent<AudioSource>().Play();
        }                                                                  
    }
}
