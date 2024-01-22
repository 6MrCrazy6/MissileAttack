using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private GameObject MissilePrefab;
    [SerializeField] private GameObject MissileLauncher;

    [SerializeField] private Texture2D cursorTexture;

    private Vector2 cursorHotpos;

    private GameControll myGameController;

    private float defendersRotationSpeed = 0.3f;
    private int defendersTurnsAmount = 0;
    private bool defendersTurnRight = false;

    public struct Position
    {
        public Position(float X, float Y)
        {
            x = X;
            y = Y;
        }
        public float x { get;  }
        public float y { get; }
    }

    public Position getMissleLauncherPosition() 
    {
        return new Position(this.MissileLauncher.transform.position.x, this.MissileLauncher.transform.position.y);  
    }

    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameControll>();

        cursorHotpos = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, cursorHotpos, CursorMode.Auto);
    }
    void Update()
    {
        Vector3 object_pos = Camera.main.WorldToScreenPoint(MissileLauncher.transform.position);
        float angle = Mathf.Atan2(Input.mousePosition.y - object_pos.y, Input.mousePosition.x - object_pos.x) * Mathf.Rad2Deg;
        MissileLauncher.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defenders");

        if (defendersTurnRight)
            defendersTurnsAmount++;
        else
            defendersTurnsAmount--;

        foreach (GameObject obj in defenders)
        {
            float rotate = defendersRotationSpeed;
            if (defendersTurnRight)
                rotate = -rotate;
            obj.transform.Rotate(0, 0, rotate, Space.World);
        }

        if (defendersTurnsAmount >= 100)
            defendersTurnRight = false;
        else if (defendersTurnsAmount < -100)
            defendersTurnRight = true;

        if (Input.GetMouseButtonDown(0) && myGameController.missileLeft > 0)
        {
            Instantiate(MissilePrefab, MissileLauncher.transform.position, Quaternion.identity);
            myGameController.missileLeft--;
            myGameController.UpdateMissileLeftText();
        }
    }
}
