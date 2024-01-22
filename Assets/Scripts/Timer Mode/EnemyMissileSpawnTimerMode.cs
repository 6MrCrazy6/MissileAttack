using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawnTimerMode : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float Ypadding = 1f;

    private float minX, maxX;

    public float delayBetweenMissiles = 1f;

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
        while (true)
        {
            float randomX = Random.Range(minX, maxX);

            GameObject newMissile = Instantiate(enemyPrefab, new Vector3(randomX, yValue + Ypadding, 0), Quaternion.identity);

            yield return new WaitForSeconds(delayBetweenMissiles);
        }
    }
}
