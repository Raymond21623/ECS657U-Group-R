using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip menuMusic; 
    public AudioClip gameplayMusic;
    public GameObject mainMenuCanvas;

void Start()
{
    if (GameStateManager.isRestarting)
    {
        PlayGame();
        GameStateManager.isRestarting = false; 
    }
    else
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mainMenuCanvas.SetActive(true); 

        if (audioSource != null && menuMusic != null)
        {
            audioSource.clip = menuMusic;
            audioSource.Play();
        }
    }
}

    public void SelectDifficulty(string difficulty)
    {
        DifficultyManager.SetDifficulty(difficulty);
        PlayGame();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        mainMenuCanvas.SetActive(false);

        if (audioSource != null && gameplayMusic != null)
        {
            audioSource.clip = gameplayMusic;
            audioSource.Play();
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
