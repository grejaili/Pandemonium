﻿using UnityEngine;
using System.Collections;

public class BgScroller : MonoBehaviour {
    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(0, Time.time * speed);

        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }

    //set speed variable
    public void setSpeed(float i)
    {
        speed = i;
    }

    public float GetSpeed(){
        return speed;
    }
}
