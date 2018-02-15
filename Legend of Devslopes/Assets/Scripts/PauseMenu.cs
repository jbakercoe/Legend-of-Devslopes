using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    [SerializeField] GameObject MenuUI;
    [SerializeField] GameObject confirmationMenu;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                pause();
            }
        }
	}

    void pause()
    {
        MenuUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioManager.Instance.PauseVolume();
    }

    public void Resume()
    {
        MenuUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        AudioManager.Instance.PauseVolume();
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync("Gameplay");
        MenuUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Quit()
    {
        confirmationMenu.gameObject.SetActive(true);
    }

    public void DontQuit()
    {
        confirmationMenu.gameObject.SetActive(false);
    }

    public void ConfirmedQuit()
    {
        print("Quitting game...");
        Application.Quit();
    }

}
