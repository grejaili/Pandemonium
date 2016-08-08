using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void GoToGameScene()
    {
        
        SceneManager.LoadScene("Game_Level");
        
    }


    public void QuitGame()
    {

        Application.Quit();

    }
}
