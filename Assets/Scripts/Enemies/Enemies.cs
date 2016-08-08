using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour {
    #region Private Variables
    [SerializeField]
   protected int health;
    GameObject leftLane, rightLane, middleLane;
   
    #endregion

[SerializeField]
    protected int damage;
    protected Lane currentLane, targetLane;
    [SerializeField]
    protected float moveSpeed;
    protected GameObject playerTmp;
    protected Animator anim, playerAnim;
    Vector3 mousePos;
    bool clickedRightSide = false;
    bool playedExplodeSound = false;
    protected AudioSource aSource;
    public AudioClip hitSound, boatExplosion;

    public void FindLanes(WorldObjects wObjects )
    {
        leftLane = wObjects.LeftLane();
        rightLane = wObjects.RightLane();
        middleLane = wObjects.MiddleLane();
    }

    void OnMouseDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > playerTmp.transform.position.x)
        {
            clickedRightSide = true;
            Debug.Log("Right");
        }
        else
        {
            clickedRightSide = false;
            Debug.Log("Left");
        }
        LookAt();
        health -= 1;
        aSource.PlayOneShot(hitSound);
        if (health <= 0)
        {
            currentLane.MakeAvailableFromEnemy();
            if (!playedExplodeSound)
            {
                aSource.PlayOneShot(boatExplosion);
                playedExplodeSound = true;
            }
            DestroyEnemy();
        }
        Debug.Log(anim);
        
       
        playerAnim.SetTrigger("Attack");
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Lane")
        {
            currentLane= col.gameObject.GetComponent<Lane>();
        }
    }
    public void DestroyEnemy()
    {
        //animation  
        StopAllCoroutines();
        Destroy(this.gameObject,1.25f);
        
    }
    protected void MoveForward()
    {
        gameObject.transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
    protected void TargetMove(Transform targetLocation)
    {
        transform.position = Vector3.MoveTowards(transform.position,
                                 targetLocation.position,
                                 moveSpeed * Time.deltaTime);
    }
    // ToDO:
    // Change for in Trigger enter;
    
    void LookAt()
    {

        Transform transformTmp = playerTmp.transform;
        Vector3 newLocalScale = new Vector3(transformTmp.localScale.x, transformTmp.localScale.y,transformTmp.localScale.z);
     
        if (clickedRightSide)
        {
            Debug.Log("hit right");
            newLocalScale.x = 0.33f;
        }
        else
        {
            Debug.Log("hit left");
            newLocalScale.x = -0.33f;
        }

        transformTmp.localScale = newLocalScale;
  
    }
    public void StopAtack()
    {
        
        StopAllCoroutines();
    }

}
