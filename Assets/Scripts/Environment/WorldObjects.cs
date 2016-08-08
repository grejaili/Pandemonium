using UnityEngine;
using System.Collections;

public class WorldObjects : MonoBehaviour {
    [SerializeField]
    private GameObject leftLane, rightLane, middleLane;
    [SerializeField]
    private Transform botSpawnPos;

    public GameObject LeftLane()
    {
        return leftLane;
    }
    public GameObject RightLane()
    {
        return rightLane;
    }
    public GameObject MiddleLane()
    {
        return middleLane;
    }
    public Transform BotSpawnPosition()
    {
        return botSpawnPos;
    }


    }
    
