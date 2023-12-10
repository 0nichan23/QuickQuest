using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] private Damageable damageable;
    [SerializeField] private DamageDealer dealer;
    [SerializeField] private UnitData unitData;
    [SerializeField] private UnitAttackHandler attackHandler;

    public Damageable Damageable { get => damageable; }
    public DamageDealer Dealer { get => dealer; }
    public UnitData UnitData { get => unitData; }

    private void Start()
    {
        damageable = new Damageable(this);
        dealer = new DamageDealer(this);
        if (!ReferenceEquals(attackHandler, null))
            attackHandler?.CacheOwner(this);
    }



}
