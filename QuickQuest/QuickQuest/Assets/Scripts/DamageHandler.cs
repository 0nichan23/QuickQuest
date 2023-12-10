using System.Collections.Generic;
using UnityEngine;

public class DamageHandler
{
    private int baseDamage;
    private List<float> damageModifiers = new List<float>();

    public DamageHandler(int baseDamage)
    {
        this.baseDamage = baseDamage;
    }

    public void AddMod(float givenMod)
    {
        damageModifiers.Add(givenMod);
    }

    public int CalcFinalDamageMult()
    {
        float amount = baseDamage;
        foreach (var item in damageModifiers)
        {
            if (item == 0)
            {
                amount = 0;
                break;
            }
            else if (item >= 1)
            {
                amount += (item * baseDamage) - baseDamage;//add damage
            }
            else
            {
                amount -= baseDamage - (item * baseDamage);//reduce damage
            }
        }
        return Mathf.RoundToInt(Mathf.Clamp(amount, 0, amount));//aint messing with them floats this time
    }
}
