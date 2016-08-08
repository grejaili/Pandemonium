using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    protected float moveSpeed;
    GameManager gm;
    bool hitPlayer = false;

	// Use this for initialization
	
	
	// Update is called once per frame

    void Update()
    {
        Move();
    }
    void OnTriggerEnter(Collider col)
    {
        if ( col.gameObject.tag == "Player")
        {
            Debug.Log("RockHitsPlayer");
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            hitPlayer = true;
        }
        if (col.gameObject.tag == "GarbageCollector")
        {
            if (!hitPlayer)
            {
                gm.AddToScore(25);
            }

            Destroy(this.gameObject);
        }
    }
    protected void Move()
    {
        gameObject.transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

    public void SetGm(GameManager gameManager)
    {
        gm = gameManager;
    }

    public void Mirror()
    {
       transform.localScale = new Vector3(-transform.localScale.x,1,1);
    }

    public void IncreaseObstacleSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public float MoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float amount){
        moveSpeed = amount;
    }
}
