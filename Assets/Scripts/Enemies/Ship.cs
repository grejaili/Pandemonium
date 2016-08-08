using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : Enemies {
    bool isAttacking = false;
    float shipXPosition;
    Lane playerLane;
    
    PlayerMovement pm;
    PlayerHealth pHealth;
    bool hasReachedFirstPosition;
    float strafeSpeed = 10;
    Vector3 newScale;
    bool facingLeft;

   
  
    void Start () {
        anim = GetComponentInChildren<Animator>();
        playerTmp = GameObject.FindGameObjectWithTag("Player");
        playerAnim = playerTmp.GetComponentInChildren<Animator>();
        pm = playerTmp.GetComponent<PlayerMovement>();
        playerLane = pm.GetPlayerLane();
        pHealth = playerTmp.GetComponent<PlayerHealth>();
        targetLane = GenerateTargetPos();
        newScale = new Vector3(transform.localScale.x,transform.localScale.y,transform.localScale.z);
        aSource = GetComponent<AudioSource>();
      
	}
	
	// Update is called once per frame
	void Update () {
        ControllAnimations();
        //if enemie is next to the player 

        playerLane = pm.GetPlayerLane();

        if (transform.position.x>playerTmp.transform.position.x && facingLeft)
        {
            newScale.x = -newScale.x;
            transform.localScale = newScale;
            facingLeft = false;
            
        }

        else if (transform.position.x < playerTmp.transform.position.x && !facingLeft)
        {
            newScale.x = -newScale.x;
            transform.localScale = newScale;
            facingLeft = true;
        }

        if (playerLane.NearbyLanes().Contains(targetLane))
        {
            if (!targetLane.Occupied())
            {
                if (transform.position != targetLane.gameObject.transform.position)
                {
                       if (targetLane.gameObject.transform.position.x < transform.position.x)
                       {
                           //MOVING LEFT ANIMATION
                       }
                        
                       else if(targetLane.gameObject.transform.position.x > transform.position.x)
                       {
                           //MOVING RIGHT ANIMATION
                       }

                        TargetMove(targetLane.gameObject.transform);                
                }

                //Attacking Player
                else
                {
                    if (!isAttacking)
                    {
                        
                        StartCoroutine(CR_Attack());
                    }
                }
            }
            else
            {
                if (moveSpeed < strafeSpeed)
                    moveSpeed = strafeSpeed;             
                targetLane = GenerateTargetPos();
            }
        }

        else
        {
            targetLane = GenerateTargetPos();
        }
  
	}
   
    IEnumerator CR_Attack()
    {
       isAttacking = true;
       
       yield return new WaitForSeconds(2.5f);
       anim.SetTrigger("Attack");
       yield return new WaitForSeconds(0.25f);
       pHealth.TakeDamage(1);      
       isAttacking = false;
    }

    Lane GenerateTargetPos()
    {
       return playerLane.NearbyLanes()[Random.Range(0, playerLane.NearbyLanes().Count)];
    }


    void ControllAnimations()
    {
        if (health == 2)
        {
            anim.SetBool("Damage1", true);
        }
        if (health == 1)
        {
            anim.SetBool("Damage2", true);
        }
        if (health <= 0)
        {
            anim.SetBool("Dead", true);
            
        }

    }


}
