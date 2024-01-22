using System.Collections;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float Ypadding = 1f;

    private float minX, maxX;

    public int missileToSpawnThisRound = 10;
    public float delayBetweenMissiles = 20f;

    float yValue;

    void Awake()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        float randomX = Random.Range(minX, maxX);
        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

    }

    public void StartRound()
    {
        StartCoroutine(SpawnMissiles());
    }

    public IEnumerator SpawnMissiles()
    {
        while (missileToSpawnThisRound > 0.5)
        {
            float randomX = Random.Range(minX, maxX);

            GameObject newMissile = Instantiate(enemyPrefab, new Vector3(randomX, yValue + Ypadding, 0), Quaternion.identity);

            missileToSpawnThisRound--;

            yield return new WaitForSeconds(delayBetweenMissiles);
        }
    }
}