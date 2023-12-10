using System;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class Damageable
{
    private BaseUnit owner;
    public Action<Damageable, DamageDealer, DamageHandler> OnGetHit;
    public Action<Damageable> OnDeath;

    private int maxHp;
    private int currentHp;
    public BaseUnit Owner { get => owner; }

    public Damageable(BaseUnit unit)
    {
        owner = unit;
        maxHp = unit.UnitData.Hp;
        currentHp = maxHp;
        OnDeath += DisableObject;
    }

    private void DisableObject(Damageable dmg)
    {
        dmg.owner.gameObject.SetActive(false);
    }

    private void TurnOffGo(Damageable damageable)
    {
        damageable.owner.gameObject.SetActive(false);
    }

    public void GetHit(DamageHandler dmg, DamageDealer dealer)
    {
        OnGetHit?.Invoke(this, dealer, dmg);
        dealer.OnHit?.Invoke(this, dealer, dmg);
        currentHp -= dmg.CalcFinalDamageMult();
        Debug.Log(owner.gameObject.name + $" took {dmg.CalcFinalDamageMult()} damage");
        if (currentHp <= 0)
        {
            OnDeath?.Invoke(this);
            dealer.OnKill?.Invoke(this, dealer);
            Debug.Log(owner.gameObject.name + $" was killed by {dealer.Owner.gameObject.name}");
        }
        ClampHealth();
    }

    private void ClampHealth()
    {
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
    }


}


