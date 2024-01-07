using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas;

    void Start()
    {
        this.gameObject.SetActive(false); 
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        GameStateManager.isRestarting = true; // Indicate that the game is restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
        {
            GameStateManager.isRestarting = false;
            this.gameObject.SetActive(false); 
            if (mainMenuCanvas != null)
            {
                mainMenuCanvas.SetActive(true);  // Activate the main menu canvas
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    public void ExitGame()
    {
        Application.Quit();
    }
}
