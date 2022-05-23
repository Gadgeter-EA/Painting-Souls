using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject Player;
    [SerializeField] AudioMixer audioMixer;

    // Update is called once per frame

    private void Start()
    {
        setOptions();
        FindObjectOfType<AudioManager>().Play("Detective_loop");
        FindObjectOfType<AudioManager>().Play("OfficeAmbient");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Detective_loop");
        FindObjectOfType<AudioManager>().Play("OfficeAmbient");
        Player.GetComponent<Weapon>().enabled = true;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        FindObjectOfType<AudioManager>().Pause("Detective_loop");
        FindObjectOfType<AudioManager>().Pause("OfficeAmbient");
        Player.GetComponent<Weapon>().enabled = false;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Bye");
        Application.Quit();
    }

    private void setOptions()
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume")) * 20);
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
    }
    
    
}
