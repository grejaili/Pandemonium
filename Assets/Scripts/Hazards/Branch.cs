using UnityEngine;
using System.Collections;

public class Branch : Obstacle {

	// Use this for initialization

    void Start()
    {
        moveSpeed = (-moveSpeed);
    }
	
	
}
