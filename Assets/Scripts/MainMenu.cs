using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("mainMenuTheme");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame()
    {
        Debug.Log("Bye");
        Application.Quit();
    }
}
