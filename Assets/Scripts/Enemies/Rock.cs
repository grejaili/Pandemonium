using UnityEngine;
using System.Collections;

public class Rock : Obstacle {


    void Start()
    {
        moveSpeed = (-moveSpeed);
    }
    
}
