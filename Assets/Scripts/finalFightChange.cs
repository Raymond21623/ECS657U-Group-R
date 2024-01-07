using UnityEngine;

public class finalFightChange : MonoBehaviour
{
    public AudioClip newGameplayMusic;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Music PLayer Found: ");

            changeMusic();
        }
    }

    private void changeMusic()
    {
        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu != null)
        {
            pauseMenu.ChangeGameplayMusic(newGameplayMusic);
            Debug.Log("Music Changed to: " + newGameplayMusic.name);
        }
        else
        {
            Debug.LogError("PauseMenu instance not found in the scene.");
        }
    }
}
