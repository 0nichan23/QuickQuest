using UnityEngine;

public enum AttackType
{
    PROJECTILE,
    SWIPE,
    POKE,
    TARGET,
    BLAST
}

[CreateAssetMenu(fileName = "Unit", menuName = "Unit")]
public class UnitData : ScriptableObject
{
    [SerializeField] private AttackType attackType;
    [SerializeField] private int baseDamage;
    [SerializeField] private float range;
    [SerializeField] private int hp;
    [SerializeField] private float cd;
    [SerializeField] private float def;

    //list of status effects to inflict on hit, each with its respective chance


    public AttackType AttackType { get => attackType; }
    public int BaseDamage { get => baseDamage; }
    public float Range { get => range; }
    public int Hp { get => hp; }
    public float Cd { get => cd; }
    public float Def { get => def; }
}
