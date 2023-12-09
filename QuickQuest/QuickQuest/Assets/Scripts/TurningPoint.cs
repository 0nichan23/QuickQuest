using UnityEngine;

public class TurningPoint
{
    private Vector3 pos;
    private Vector2Int dir;

    public TurningPoint(Vector3 pos, Vector2Int dir)
    {
        this.pos = pos;
        this.dir = dir;
    }

    public Vector3 Pos { get => pos; }
    public Vector2Int Dir { get => dir; }
}