using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] private SnakeMovement head;
    [SerializeField] private SnakeMovement[] bodyParts;
    [SerializeField] private float distanceOfParts;

    private void Start()
    {
        if (bodyParts.Length > 0)
        {
            bodyParts[0].CachePriorBodyPart(head);
            head.OnDirChanged.AddListener(bodyParts[0].CacheTurningPoint);
            bodyParts[0].OnPointReached.AddListener(ClampDistances);
            for (int i = 1; i < bodyParts.Length; i++)
            {
                bodyParts[i].CachePriorBodyPart(bodyParts[i - 1]);
                head.OnDirChanged.AddListener(bodyParts[i].CacheTurningPoint);
                bodyParts[i].OnPointReached.AddListener(ClampDistances);
            }
        }
    }


    private void ClampDistances(Vector2Int dir, Vector3 pos, SnakeMovement bp, SnakeMovement priorBp)//this tile, tile before
    {
        Vector3 dist = new Vector2(distanceOfParts, distanceOfParts) * (dir * -1);

        bp.transform.position = priorBp.transform.position + dist;

    }

    private void Update()
    {
        Debug.Log(Vector3.Distance(head.transform.position, bodyParts[0].transform.position));
    }
}
