using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    protected Lane currentLane;
    protected bool isRespawning = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Lane GetPlayerLane()
    {
        return currentLane;
    }

    public void SetPlayerLane(Lane l)
    {
        currentLane = l;
    }

    public void PlayerRespawning()
    {
        isRespawning = true;
        Debug.Log("Respawn Movemnt");
    }
    
}
