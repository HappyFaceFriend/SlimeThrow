using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystem
{
    enum State { Default, Dead, Invincible}
    public BuffableStat MaxHp { get; private set; }
    public float CurrentHp { get; private set; }

    public bool IsDead { get; private set; }
    public bool IsInvincible { get; private set; }

    public delegate void ActionOnDie();
    public ActionOnDie OnDie;
    public HpSystem(float maxHp, ActionOnDie action)
    {
        MaxHp = new BuffableStat(maxHp);
        CurrentHp = maxHp;
        OnDie = action;
    }
    public void ChangeHp(float delta)
    {
        CurrentHp += delta;
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            IsDead = true;
            OnDie();
        }
        else if (CurrentHp > MaxHp.Value)
        {
            CurrentHp = MaxHp.Value;
        }
    }
}

