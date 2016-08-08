using UnityEngine;
using System.Collections;

public class SwipePlayerMovement : PlayerMovement
{
    public float minSwipeDistX;

    private Vector2 startPos;
    [SerializeField]
    private float moveSpeed;

    private GameObject leftLane, rightLane, middleLane;
    
    private GameObject currentPos, destinationPos;
    private bool isMoving = false;

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
//#if UNITY_ANDROID
		if (Input.touchCount > 0) 			
		{			
			Touch touch = Input.touches[0];			
			switch (touch.phase) 
				
			{				
			case TouchPhase.Began:
				startPos = touch.position;				
				break;
								
			case TouchPhase.Ended:				
					float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
					
					if (swipeDistHorizontal > minSwipeDistX) 
						
					{
						
						float swipeValue = Mathf.Sign(touch.position.x - startPos.x);

                        if (swipeValue <0 && !isMoving)//right swipe
                        {
                            if (currentPos == middleLane)
                                destinationPos = leftLane;
                            else if (currentPos == rightLane)
                                destinationPos = middleLane;
                        }



                        else if (swipeValue > 0 && !isMoving) //left swipe
                        {
                            if (currentPos == middleLane)
                                destinationPos = rightLane;
                            else if (currentPos == leftLane)
                                destinationPos = middleLane;
                        }
							
							
						
					}
				break;
			}
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

   

    

}