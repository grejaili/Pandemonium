using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    [SerializeField]
    float health;
    bool isBeingAttacked = false , isRespawning = false;
    private SpriteRenderer renderer;
    Animator anim;
    public GameManager gm;
    public GameObject boat;
    public Transform botSpawnPos;
    PlayerMovement pm;
    public Image[] lifeImages;
    int lifeImgIndex;
    bool isDead = false;
    AudioSource aSource;
    public AudioClip pandaDmg, pandaDeath;
    
	// Use this for initialization
    void Awake()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
        renderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        lifeImgIndex = lifeImages.Length-1;
        aSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	
    public void RegenHealth(int powerUp)
    {
        health += powerUp;
    }
    public void TakeDamage(int damage)
    {
        if (!isBeingAttacked && !isRespawning)
        {
            aSource.PlayOneShot(pandaDmg);
            health -= damage;
            if (lifeImgIndex>=0)
            {
                lifeImages[lifeImgIndex].enabled = false;
                lifeImgIndex--;
            }
            if (health<= 0)
            {
                isDead = true;
                anim.SetBool("Dead", isDead);
                if (gm.LoseLife())
                {
                    aSource.PlayOneShot(pandaDeath);
                    if (!isRespawning)
                    {
                        StartCoroutine(CR_Respawn());
                    }
                }
            }
            else
            {
                anim.SetTrigger("Damage");
               
            }
            StartCoroutine(CR_ShowDamage());
        }
    }
    
    IEnumerator CR_ShowDamage()
    {
        isBeingAttacked = true;
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        renderer.color = Color.white;
        isBeingAttacked = false;
    }

    IEnumerator CR_Respawn()
    {
        isRespawning = true;
        yield return new WaitForSeconds(0.65f);
        boat.SetActive(false);
        gm.DestroyEnemies();
        health = 3;
        yield return new WaitForSeconds(1.25f);
        isDead = false;
        anim.SetBool("Dead", isDead);       
        for (int i = 0; i < lifeImages.Length; i++)
        {
            lifeImages[i].enabled = true;
        }
        lifeImgIndex = lifeImages.Length-1;
        gameObject.transform.position = botSpawnPos.position;
        boat.SetActive(true);
        anim.SetTrigger("Respawn");
        pm.PlayerRespawning();
        yield return new WaitForSeconds(1f);
        gm.StartSpawningEnemies();
        isRespawning = false;

        
        

    }


}
