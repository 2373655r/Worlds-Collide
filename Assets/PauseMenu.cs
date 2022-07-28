using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    

    public static bool GamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject controlsUI;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GamePaused)
            {
                Resume();
            }
            else
            {
               Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void OpenControls()
    {
        pauseMenuUI.SetActive(false);
        controlsUI.SetActive(true);
    }

    public void CloseControls()
    {
        controlsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
