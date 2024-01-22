using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class EnemyMissle : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] defenders;

    private GameControll myGameControll;

    Vector3 target;

    private void Awake()
    {
        myGameControll = GameObject.FindObjectOfType<GameControll>();
    }

    void Start()
    {
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = defenders[Random.Range(0, defenders.Length)].transform.position;
        speed = myGameControll.enemyMissileSpeed;

        if(defenders.Length <= 0)
        {
            myGameControll.GameOver();
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
       if (!myGameControll.isGameOver)
       {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
       }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Defenders")
        {
            myGameControll.EnemyMissilesDestroyed();
            missileExplode();
            myGameControll.spaceHouseCounter--;
            Destroy(collision.gameObject);
        }

        if(collision.tag == "Explosion")
        {
            //This will add the points for destroyed an enemy missiles
            myGameControll.AddMissileDestroyScore();
            missileExplode();
        }

        if(collision.tag == "Floor")
        {
            Destroy(gameObject);
            myGameControll.EnemyMissilesDestroyed();
        }
    }

    //Spawn explosion and destroy missiles
    private void missileExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

