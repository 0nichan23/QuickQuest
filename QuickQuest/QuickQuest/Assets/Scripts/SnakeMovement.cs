using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] Rigidbody2D rb;
    private Vector2Int moveDir;

    private void Start()
    {
        moveDir = new Vector2Int(0, 1);//move upwards by default
    }

    private void Update()
    {
        TempHandleInput();
        rb.velocity = new Vector2(moveDir.x, moveDir.y) * movementSpeed;

    }

    private void TempHandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && moveDir.y != -1)
        {
            moveDir = new Vector2Int(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) && moveDir.y != 1)
        {
            moveDir = new Vector2Int(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A) && moveDir.x != 1)
        {
            moveDir = new Vector2Int(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D) && moveDir.x != -1)
        {
            moveDir = new Vector2Int(1, 0);
        }
    }
}
