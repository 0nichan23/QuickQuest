using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Progress;

public class UnitAttackHandler : MonoBehaviour
{
    private List<BaseUnit> legalTargets = new List<BaseUnit>();
    [SerializeField] private CircleCollider2D rangeDetectioin;
    private BaseUnit owner;
    private float cdr;
    private float lastAttacked;
    public BaseUnit Owner { get => owner; }
    public float Cdr { get => Mathf.Clamp( cdr, 0.1f, 1f); }

    private void Start()
    {
        lastAttacked = 0;
        cdr = 1;
    }
    public void CacheOwner(BaseUnit unit)
    {
        owner = unit;
        rangeDetectioin.radius = owner.UnitData.Range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseUnit unit = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(unit, null))
        {
            legalTargets.Add(unit);
            Debug.Log("added legal target");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BaseUnit unit = collision.GetComponent<BaseUnit>();
        if (!ReferenceEquals(unit, null))
        {
            legalTargets.Remove(unit);
        }
    }

    public BaseUnit GetClosestUnit()
    {
        float distance = Vector3.Distance(legalTargets[0].transform.position, transform.position);
        BaseUnit closest = legalTargets[0];
        foreach (var item in legalTargets)
        {
            float newDist = Vector3.Distance(item.transform.position, transform.position);
            if (newDist <= distance)
            {
                distance = newDist;
                closest = item;
            }
        }
        return closest;
    }

    public float GetCD()
    {
        return owner.UnitData.Cd * Cdr;
    }

    private void Update()
    {
        if (legalTargets.Count > 0)
        {
            AttackUnit(GetClosestUnit());
        }
    }

    private void AttackUnit(BaseUnit target)
    {
        if (Time.time - lastAttacked < GetCD())
        {
            return;
        }
        target.Damageable.GetHit(new DamageHandler(owner.UnitData.BaseDamage), owner.Dealer); 
        lastAttacked = Time.time;

    }

}
