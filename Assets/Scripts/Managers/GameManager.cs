using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private Enemies[] enemyPrefabs;
    List<Enemies> currEnemies;
    [SerializeField]
    private WorldObjects wObjects;
	private GameObject leftLane, rightLane, middleLane;
    private bool isSpawningEnemy = false;
    private List<Lane> availableLanes;
    [SerializeField]
    private Canvas pauseMenu, gameOverMenu;
    public int lives = 3;
    float score;
    bool lostLife = false;
    public Text scoreText, livesText;
    AudioSource aSource;
    void Awake()
    {
        leftLane = wObjects.LeftLane();
        rightLane = wObjects.RightLane();
        middleLane = wObjects.MiddleLane();
        availableLanes = new List<Lane>();
        availableLanes.Add(leftLane.GetComponent<Lane>());
        availableLanes.Add(rightLane.GetComponent<Lane>());
        availableLanes.Add(middleLane.GetComponent<Lane>());
        currEnemies = new List<Enemies>();
        aSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
        score += 6.5f * Time.deltaTime; 
        if (!isSpawningEnemy &&  availableLanes.Count > 0)
        {
            StartCoroutine(CR_SpawnEnemyOnTimer());
        }

        if (score > 0)
        {
            scoreText.text = "SCORE: " + Mathf.FloorToInt(score).ToString();
        }
	}

    IEnumerator CR_SpawnEnemyOnTimer()
    {
        if (availableLanes.Count > 0)
        {
            int randPosNum = Random.Range(0, availableLanes.Count);
            Vector3 spawnLocation = new Vector3(availableLanes[randPosNum].gameObject.transform.position.x,
                wObjects.BotSpawnPosition().position.y,
                wObjects.BotSpawnPosition().position.z);
            isSpawningEnemy = true;
            Enemies tempEnemy = Instantiate(enemyPrefabs[0], spawnLocation, enemyPrefabs[0].transform.rotation) as Enemies;
            tempEnemy.FindLanes(wObjects);
            currEnemies.Add(tempEnemy);
            yield return new WaitForSeconds(5);
            isSpawningEnemy = false;
        }
    }

    public void MakeLaneUnavailable(Lane lane)
    {
        if (availableLanes.Contains(lane))
        {
            availableLanes.Remove(lane);
        }
    }

    public void MakeLaneAvailable(Lane lane)
    {
        if (!availableLanes.Contains(lane))
        {
            availableLanes.Add(lane);
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
        aSource.Pause();

    }
    public void ClosePauseMenu()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        aSource.UnPause();
        
    }

    public void AddToScore(int num)
    {
        score += num;
    }

    public void DestroyEnemies()
    {
        isSpawningEnemy = true;
        StopAllCoroutines();
        for (int i=0; i < currEnemies.Count; i++)
        {
            if (currEnemies[i] != null)
            {
                currEnemies[i].DestroyEnemy();
            }
        }
    }
    public bool LoseLife()
    {
     
            lives--;
            if (lives > 0)
            {
                livesText.text = "X" + lives.ToString();
                return true;

            }
            else
            {

                livesText.text = "X" + lives.ToString();
                aSource.Pause();
                Time.timeScale = 0;
                gameOverMenu.enabled = true;
                return false;
            }
            
        

    }

    public void StartSpawningEnemies()
    {
        isSpawningEnemy = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game_Level");
        Time.timeScale = 1; 
    }
    public void QuitGame()
    {

        Application.Quit();

    }
}
