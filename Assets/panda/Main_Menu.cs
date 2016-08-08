using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour

{



    public void Quit()
    {
        Debug.Log("hellog");
        Application.Quit();
    }

    public void LoadLevel()
    {
        Debug.Log("hello");
        Application.LoadLevel(1);
    }
}
