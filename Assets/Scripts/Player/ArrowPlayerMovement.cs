using UnityEngine;
using System.Collections;

public class ArrowPlayerMovement : PlayerMovement
{
    [SerializeField]
    private float moveSpeed;

    private GameObject leftLane, rightLane, middleLane;
    
    private GameObject currentPos, destinationPos;
    private bool isMoving =false;

    void Awake()
    {
        leftLane = GetComponentInParent<WorldObjects>().LeftLane();
        rightLane = GetComponentInParent<WorldObjects>().RightLane();
        middleLane = GetComponentInParent<WorldObjects>().MiddleLane();
        currentPos = middleLane;
        currentLane = currentPos.GetComponent<Lane>();
        destinationPos = middleLane;
        transform.position = middleLane.transform.position;
    }

    void Update()
    {
        if (!isRespawning)
        {
            // Debug.Log("Current Pos: " + currentPos.transform.position.x);
            //Debug.Log("Dest Pos: " + destinationPos.transform.position.x);
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
            {
                if (currentPos == middleLane)
                    destinationPos = leftLane;
                else if (currentPos == rightLane)
                    destinationPos = middleLane;

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
            {
                if (currentPos == middleLane)
                    destinationPos = rightLane;
                else if (currentPos == leftLane)
                    destinationPos = middleLane;

            }

            if (transform.position != destinationPos.transform.position)
            {

                isMoving = true;

                transform.position = Vector3.MoveTowards(transform.position,
                                     destinationPos.transform.position,
                                     moveSpeed * Time.deltaTime);
            }
            else
            {

                isMoving = false;
                currentPos = destinationPos;

            }
        }
        else
        {
            if (transform.position != middleLane.transform.position)
            {
            transform.position = Vector3.MoveTowards(transform.position,
                                    middleLane.transform.position,
                                    moveSpeed * Time.deltaTime);
            }
            else
            {
              isRespawning = false;
            }

        }
    }

       
     

   

  
}
