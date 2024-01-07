using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainMenuCanvas;
    public GameObject healthCanvas; // Reference to the health canvas
    public static bool isPaused;

    public AudioSource audioSource; 
    public AudioClip gameplayMusic;
    public AudioClip pauseMenuMusic; 

    void Start()
    {
        pauseMenu.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        if (healthCanvas != null) healthCanvas.SetActive(false); // Deactivate health canvas
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (audioSource != null && pauseMenuMusic != null)
        {
            audioSource.clip = pauseMenuMusic;
            audioSource.Play();
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        if (healthCanvas != null) healthCanvas.SetActive(true); // Reactivate health canvas
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (audioSource != null && gameplayMusic != null)
        {
            audioSource.clip = gameplayMusic;
            audioSource.Play();
        }
    }

    public void GoToMainMenu()
    {
        pauseMenu.SetActive(false);
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true); // Activate the main menu canvas
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        Time.timeScale = 0f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ChangeGameplayMusic(AudioClip newMusic)
    {
        Debug.Log("Music Being Changed");
        gameplayMusic = newMusic;
        if (audioSource != null)
        {
            audioSource.clip = gameplayMusic;
            audioSource.Play();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
