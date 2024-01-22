using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject MissileLauncher;

    private Vector2 target;

    void Start()
    {
        Touch touch = Input.GetTouch(0);
        target = Camera.main.ScreenToWorldPoint(touch.position);
        float angle = Mathf.Atan((target.y - MissileLauncher.transform.position.y) / (target.x - MissileLauncher.transform.position.x)) * (180 / Mathf.PI);
        if (angle < 90 && angle > 0)
        {
            angle -= 90;
        }
        else
        {
            angle += 90;
        }
        this.gameObject.transform.Rotate(0.0f, 0.0f, angle, Space.Self);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
        if (transform.position == (Vector3)target)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
