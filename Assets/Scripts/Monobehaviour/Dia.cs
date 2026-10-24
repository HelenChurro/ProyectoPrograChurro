using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [Header("Ciclo")]
    [SerializeField] private float dayDuration = 20f;
    [SerializeField] private Spawner spawner;
    [SerializeField] private int nightBossCount = 3; 

    private bool isDay = true;
    private List<GameObject> currentNightEnemies = new List<GameObject>();
    private Coroutine dayCoroutine;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            // --- FASE DE DÍA ---
            isDay = true;
            Debug.Log("--- DÍA ---");

            // Iniciar spawns de día por corutina
            dayCoroutine = StartCoroutine(spawner.DaySpawningRoutine(dayDuration));
            yield return new WaitForSeconds(dayDuration);

            // Detener spawns de día si seguían corriendo
            if (dayCoroutine != null) StopCoroutine(dayCoroutine);

            // --- FASE DE NOCHE ---
            isDay = false;
            Debug.Log("--- NOCHE ---");

            
            currentNightEnemies = spawner.SpawnNightWave(nightBossCount);

            
            yield return new WaitUntil(() => currentNightEnemies.Count == 0);

            Debug.Log("___FIN___");
            yield return new WaitForSeconds(2f); //Descansito
        }
    }

    
    public void OnEnemyKilled(Enemy enemy)
    {
        if (!isDay && currentNightEnemies.Contains(enemy.gameObject))
        {
            currentNightEnemies.Remove(enemy.gameObject);
            Debug.Log("Enemigo Xb.  ");
        }
    }
}
