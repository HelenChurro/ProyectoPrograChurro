using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemis")]
    [SerializeField] private GameObject dayEnemyPrefab;
    [SerializeField] private GameObject nightBossPrefab;

    [Header("Spawners")]
    [SerializeField] private Transform[] spawnPositions;

    private List<GameObject> activeNightEnemies = new List<GameObject>();

    // Día
    public IEnumerator DaySpawningRoutine(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            if (spawnPositions.Length > 0 && dayEnemyPrefab != null)
            {
                int randomIndex = Random.Range(0, spawnPositions.Length);
                Instantiate(dayEnemyPrefab, spawnPositions[randomIndex].position, Quaternion.identity);
                Debug.Log("Spawneando enemigo de día al azar.");
            }

            // espera
            float waitTime = Random.Range(4f, 8f);
            yield return new WaitForSeconds(waitTime);
            timer += waitTime;
        }
    }

    // Nochecita Enemis grandes
    public List<GameObject> SpawnNightWave(int amount)
    {
        activeNightEnemies.Clear();

        for (int i = 0; i < amount; i++)
        {
            if (spawnPositions.Length > 0 && nightBossPrefab != null)
            {
                int randomIndex = Random.Range(0, spawnPositions.Length);
                GameObject boss = Instantiate(nightBossPrefab, spawnPositions[randomIndex].position, Quaternion.identity);

                //Noche
                Enemy enemyScript = boss.GetComponent<Enemy>();
                if (enemyScript != null) enemyScript.isNightBoss = true;

                activeNightEnemies.Add(boss);
            }
        }

        Debug.Log("¡OLEADA son {amount} enemigos!");
        return activeNightEnemies;
    }
}