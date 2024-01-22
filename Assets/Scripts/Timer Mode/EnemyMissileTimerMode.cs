using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileTimerMode : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private GameObject explosionPrefab;

    GameObject[] defenders;

    private GameControllerTimerMode myControllerTimerMode;

    Vector3 target;

    private void Awake()
    {
        myControllerTimerMode = GameObject.FindObjectOfType<GameControllerTimerMode>();
    }

    void Start()
    {
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = defenders[Random.Range(0, defenders.Length)].transform.position;
        speed = myControllerTimerMode.enemyMissileSpeed;

        if (defenders.Length <= 0)
        {
            myControllerTimerMode.GameOver();
        }

        Debug.Log(speed);
        float angle = Mathf.Atan((target.y - gameObject.transform.position.y) / (target.x - gameObject.transform.position.x)) * (180 / Mathf.PI);
        if (angle < 90 && angle > 0)
        {
            angle += 90;
        }
        else
        {
            angle -= 90;
        }

        this.gameObject.transform.Rotate(0.0f, 0.0f, angle, Space.Self);
    }


    void Update()
    {
        if (!myControllerTimerMode.isGameOver)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Defenders")
        {
            missileExplode();
            myControllerTimerMode.spaceHouseCounter--;
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Explosion")
        {
            //This will add the points for destroyed an enemy missiles
            myControllerTimerMode.AddMissileDestroyScore();
            myControllerTimerMode.destroyedEnemyMissile++;
            missileExplode();
        }

        if (collision.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }

    //Spawn explosion and destroy missiles
    private void missileExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
