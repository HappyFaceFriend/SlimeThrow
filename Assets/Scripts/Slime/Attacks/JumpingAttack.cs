using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingAttack : SlimeAttackBase
{
    [SerializeField] GameObject _attackEffect;

    public void AnimEvent_JumpingAttack()
    {
        GameObject effect = Instantiate(_attackEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
