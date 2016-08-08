using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lane : MonoBehaviour {

    public bool isPlayerOccupied = false;

    [SerializeField]
    private GameManager gm;
    [SerializeField]
    Lane[] nearbyLanes;
    List<Lane> nLaneList;

    void Awake()
    {
        nLaneList = new List<Lane>();
        for (int i =0; i<nearbyLanes.Length; i++)
        {
            nLaneList.Add(nearbyLanes[i]);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" )
        {
            Debug.Log("YO");
            isPlayerOccupied = true;
            col.gameObject.GetComponent<PlayerMovement>().SetPlayerLane(this);
            gm.MakeLaneUnavailable(this);
        }

        if (col.gameObject.tag == "Enemy")
        {
            gm.MakeLaneUnavailable(this);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPlayerOccupied = false;
            gm.MakeLaneAvailable(this);
        }
        if (col.gameObject.tag == "Enemy")
        {
            gm.MakeLaneAvailable(this);
        }
    }


    public void MakeAvailableFromEnemy()
    {
       
        gm.MakeLaneAvailable(this);
    }
    public void MakeUnavailableFromPlayer()
    {
        isPlayerOccupied = true;
        gm.MakeLaneUnavailable(this);
    }

    public void MakeAvailableFromPlayer()
    {
        isPlayerOccupied = false;
        gm.MakeLaneAvailable(this);
    }


    public bool Occupied()
    {
        return isPlayerOccupied;
    }

    public List<Lane> NearbyLanes()
    {
        return nLaneList;
    }
}
