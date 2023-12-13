using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using static UnityEditor.Progress;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] private SnakeMovement head;
    private List<SnakeMovement> bodyParts = new List<SnakeMovement>();
    [SerializeField] private float distanceOfParts;
    [SerializeField] private SnakeMovement bodyPartPrefab;
    [SerializeField] private int teamSize;
    private void Start()
    {
        for (int i = 0; i < teamSize; i++)
        {
            CreateBodyPart();
        }
        InitializeSnake();
    }

    private void CreateBodyPart()
    {
        bodyParts.Add(Instantiate(bodyPartPrefab, transform));
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveBP();
        }
    }

    private void InitializeSnake(bool clamp = true)
    {
        if (bodyParts.Count > 0)
        {
            head.OnDirChanged.RemoveAllListeners();
            foreach (var item in bodyParts)
            {
                item.OnPointReached.RemoveAllListeners();
            }


            bodyParts[0].CachePriorBodyPart(head);
            head.OnDirChanged.AddListener(bodyParts[0].CacheTurningPoint);
            bodyParts[0].OnPointReached.AddListener(ClampDistances);

            for (int i = 1; i < bodyParts.Count; i++)
            {
                bodyParts[i].CachePriorBodyPart(bodyParts[i - 1]);
                head.OnDirChanged.AddListener(bodyParts[i].CacheTurningPoint);
                bodyParts[i].OnPointReached.AddListener(ClampDistances);
            }
            if (clamp)
            {
                ClampAllDistances();
            }
        }

    }

    private void ClampAllDistances()
    {
        ClampDistances(bodyParts[0].MoveDir, bodyParts[0].transform.position, bodyParts[0], head);
        for (int i = 1; i < bodyParts.Count; i++)
        {
            
            ClampDistances(bodyParts[i].MoveDir, bodyParts[i].transform.position, bodyParts[i], bodyParts[i - 1]);
        }
    }


    private void ClampDistances(Vector2Int dir, Vector3 pos, SnakeMovement bp, SnakeMovement priorBp)//this tile, tile before
    {
        Vector3 dist = new Vector2(distanceOfParts, distanceOfParts) * (dir * -1);

        bp.transform.position = priorBp.transform.position + dist;

    }

    [ContextMenu("remove bp")]
    public void RemoveBP()
    {
        int index = Random.Range(0, bodyParts.Count);
        bodyParts[index].gameObject.SetActive(false);
        bodyParts.RemoveAt(index);
        InitializeSnake(false);
        for (int i = index; i < bodyParts.Count; i++)
        {
            //if its a straight line essentialy
            if (bodyParts[i].transform.position.x == bodyParts[i].PriorBodyPart.transform.position.x || bodyParts[i].transform.position.y == bodyParts[i].PriorBodyPart.transform.position.y)
            {
                Debug.Log($"clamping distance between body parts");
                ClampDistances(bodyParts[i].MoveDir, bodyParts[i].transform.position, bodyParts[i], bodyParts[i].PriorBodyPart);
            }
            else
            {
                break;
            }
        }
    }


}
