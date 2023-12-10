using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DamageDealer 
{
    private BaseUnit owner;
    public UnityEvent<Damageable, DamageDealer, DamageHandler> OnHit;
    public UnityEvent<Damageable, DamageDealer> OnKill;

    public BaseUnit Owner { get => owner;}

    public DamageDealer(BaseUnit unit)
    {
        owner = unit;
    }

}
