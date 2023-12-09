using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] Rigidbody2D rb;
    public UnityEvent<Vector2Int, Vector3> OnDirChanged;
    public UnityEvent<Vector2Int, Vector3, SnakeMovement, SnakeMovement> OnPointReached;
    private Vector2Int moveDir;
    private Queue<TurningPoint> nextTurningPoints = new Queue<TurningPoint>();
    private TurningPoint nextTurningPoint;
    private SnakeMovement priorBodyPart;


    [SerializeField] private float inputCD;
    private float lastInput;

    public bool input;

    public SnakeMovement PriorBodyPart { get => priorBodyPart; }

    private void Start()
    {
        lastInput = -inputCD;
        moveDir = new Vector2Int(0, 1);//move upwards by default
    }

    private void Update()
    {
        if (input)
        {
            TempHandleInput();
        }

        if (nextTurningPoints.Count > 0 && ReferenceEquals(nextTurningPoint, null))
        {
            Debug.Log("recieved point");
            nextTurningPoint = nextTurningPoints.Dequeue();
        }
        if (!ReferenceEquals(nextTurningPoint, null) && CheckTurningPoint())
        {
            Debug.Log("reached point");
            ChangeDir(nextTurningPoint.Dir);
            transform.position = nextTurningPoint.Pos;
            OnPointReached?.Invoke(moveDir, transform.position, this, priorBodyPart);
            nextTurningPoint = null;
        }

        rb.velocity = new Vector2(moveDir.x, moveDir.y) * movementSpeed;

    }

    public void CacheTurningPoint(Vector2Int dir, Vector3 pos)
    {
        nextTurningPoints.Enqueue(new TurningPoint(pos, dir));
    }

    public void CachePriorBodyPart(SnakeMovement bp)
    {
        priorBodyPart = bp;
    }


    private bool CheckTurningPoint()
    {
        if (moveDir.x > 0 && transform.position.x >= nextTurningPoint.Pos.x)
        {
            return true;
        }
        else if (moveDir.x < 0 && transform.position.x <= nextTurningPoint.Pos.x)
        {
            return true;
        }
        else if (moveDir.y < 0 && transform.position.y <= nextTurningPoint.Pos.y)
        {
            return true;
        }
        else if (moveDir.y > 0 && transform.position.y >= nextTurningPoint.Pos.y)
        {
            return true;
        }
        return false;
    }

    public void ChangeDir(Vector2Int givenDir)
    {
        moveDir = givenDir;
        OnDirChanged?.Invoke(moveDir, transform.position);
    }

    private void TempHandleInput()
    {
        if (Time.time - lastInput < inputCD)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.W) && moveDir.y != -1)
        {
            ChangeDir(new Vector2Int(0, 1));
            lastInput = Time.time;

        }
        else if (Input.GetKeyDown(KeyCode.S) && moveDir.y != 1)
        {
            ChangeDir(new Vector2Int(0, -1));
            lastInput = Time.time;

        }
        else if (Input.GetKeyDown(KeyCode.A) && moveDir.x != 1)
        {
            ChangeDir(new Vector2Int(-1, 0));
            lastInput = Time.time;


        }
        else if (Input.GetKeyDown(KeyCode.D) && moveDir.x != -1)
        {
            ChangeDir(new Vector2Int(1, 0));
            lastInput = Time.time;

        }
    }
}
