using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Coroutine spawningCoroutine;
    [Header("Spawning")]
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform[] position;
    [SerializeField] private int amount;
    [SerializeField] private float timeBetweenSpawns;

    private void Awake()
    {
        StartSpawning();
    }
    public void StartSpawning()
    {
        if (spawningCoroutine != null) { return; }

        spawningCoroutine = StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        for (int i = 0; i < 5; i++)
        {
            int spawningLocation = Random.Range(0, position.Length - 1);
            Instantiate(enemy, position[spawningLocation].position, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
        yield return null;
    }
}
