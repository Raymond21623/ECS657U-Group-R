using UnityEngine;

public class victoryChange : MonoBehaviour
{

    public GameObject villagerPrefab;
    public Transform[] spawnPoints;

    public GameObject destroyEnemies;
    public FinalEnemy bossenemy;

    public AudioClip newGameplayMusic;

    private bool bossDefeated = false;

    private void Start()
    {
        destroyEnemies.SetActive(true);
    }


    void Update()
    {

        Debug.Log("Final Boss: " + bossenemy.GetBossHealth);
        if (!bossDefeated && bossenemy != null && bossenemy.GetBossHealth == 1)
        {
            OnBossDefeated();
            bossDefeated = true;
            destroyEnemies.SetActive(false);

        }
    }

    public void OnBossDefeated()
    {
        SpawnVillagers();
        changeMusic();
    }

    void SpawnVillagers()
    {
        float xOffset = 10f; 
        float zOffset = 10f;

        Vector3 startingPosition = new Vector3(102, 0, -195);


        for (int j = 0; j < 5; j++ )
        {
            for(int i = 0; i < 6; i++)
            {
                Vector3 newPosition = startingPosition + new Vector3(xOffset * i, 0, zOffset * j);

                // Set the rotation to 90 degrees on the y-axis
                Quaternion newRotation = Quaternion.Euler(0, 180, 0);

                // Instantiate the villager at the new position
                Instantiate(villagerPrefab, newPosition, newRotation);
            }
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
