using UnityEngine;

public class victoryChange : MonoBehaviour
{

    public GameObject villagerPrefab;
    public Transform[] spawnPoints; 
    public int villagersToSpawn = 10;

    public FinalEnemy bossenemy;

    private bool bossDefeated = false;


    void Update()
    {

        Debug.Log("Final Boss: " + bossenemy.GetBossHealth);
        if (!bossDefeated && bossenemy != null && bossenemy.GetBossHealth == 15)
        {
            OnBossDefeated();
            bossDefeated = true;
        }
    }

    public void OnBossDefeated()
    {
        SpawnVillagers();
    }

    void SpawnVillagers()
    {
        float xOffset = 10f; // The amount by which the x-coordinate will be increased for each villager

        for (int i = 0; i < villagersToSpawn; i++)
        {
            // Get a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Create a new position with the x-coordinate offset for each villager
            Vector3 newPosition = spawnPoint.position + new Vector3(xOffset * i, 0, 0);

            // Instantiate the villager at the new position
            Instantiate(villagerPrefab, newPosition, spawnPoint.rotation);
        }
    }
}
