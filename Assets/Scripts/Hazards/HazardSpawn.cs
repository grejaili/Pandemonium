using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HazardSpawn : MonoBehaviour {
    public Obstacle[] rocks, branches;
    public Transform[] lanes;
    public Transform leftSidePos, rightSidePos;
    public Transform spawnPos;
    float amountSpeedIncrease = 0;
    public GameManager gm;
    
    float amountScrollIncrease = 0;
    bool isSpawningRocks = false, isSpawningBranches = false , isIncreasingSpeed = false;
    
    // Use this for initialization


    void Start () {
         

    }
	
	// Update is called once per frame
	void Update () 
    {

        if (!isSpawningBranches)
        {
            StartCoroutine(CR_SpawnBranches());
        }
        if (!isSpawningRocks)
        {
            StartCoroutine(CR_SpawnRocks());
        }
        if(!isIncreasingSpeed)
        {
            StartCoroutine(CR_IncreaseObstacleSpeedOverTime());
        }
     
	}
    IEnumerator CR_SpawnRocks()
    {
        isSpawningRocks = true;

        // randomize Lane and Rocks
        //lane left 0/ lane middle 1/ lane right 2
        //size of the rocks depends on the array size on the inspector 
        int rocksNumber = Random.Range(0, rocks.Length);
        int laneNUmber = Random.Range(0, lanes.Length);
        Vector3 spawnLocation = new Vector3();
        spawnLocation = lanes[laneNUmber].position;
        spawnLocation.y = spawnPos.position.y;
        spawnLocation.z = spawnPos.position.z;
        yield return new WaitForSeconds(Random.Range(3.0f, 5.5f));
        Obstacle tempRock = Instantiate(rocks[rocksNumber], spawnLocation, Quaternion.identity) as Obstacle;
        tempRock.SetGm(gm);
        if (tempRock.MoveSpeed() + amountSpeedIncrease <= 15)
            tempRock.IncreaseObstacleSpeed(amountSpeedIncrease);
        else
            tempRock.SetMoveSpeed(15);
        // spawn rocks       


        isSpawningRocks = false;
    }

    IEnumerator CR_SpawnBranches()
    {
        isSpawningBranches = true;
        int branchNumber = Random.Range(0, branches.Length);
        int sideNumber = Random.Range(0, 2);
        yield return new WaitForSeconds(Random.Range(3.0f, 5.5f));
        Vector3 spawnLocation = new Vector3();
        spawnLocation = spawnPos.position;
        if (sideNumber == 0)
        {
            spawnLocation.x = rightSidePos.position.x;
            Obstacle tempBranch = Instantiate(branches[branchNumber], spawnLocation, Quaternion.identity) as Obstacle;
            tempBranch.SetGm(gm);
            if (tempBranch.MoveSpeed() + amountSpeedIncrease <= 15)
                tempBranch.IncreaseObstacleSpeed(amountSpeedIncrease);
            else
                tempBranch.SetMoveSpeed(15);
        }
        else
        {
            spawnLocation.x = leftSidePos.position.x;
            Obstacle tempBranch = Instantiate(branches[branchNumber], spawnLocation, Quaternion.identity) as Obstacle;
            tempBranch.SetGm(gm);
            tempBranch.Mirror();
            if (tempBranch.MoveSpeed() + amountSpeedIncrease <= 15)
                tempBranch.IncreaseObstacleSpeed(amountSpeedIncrease);
            else
                tempBranch.SetMoveSpeed(15);
        }

        isSpawningBranches = false; ;
    }


    IEnumerator CR_IncreaseObstacleSpeedOverTime()
    {
       isIncreasingSpeed = true;
       yield return new WaitForSeconds(13f);
       amountSpeedIncrease += 1.5f;
       isIncreasingSpeed = false;
    }
    

}

