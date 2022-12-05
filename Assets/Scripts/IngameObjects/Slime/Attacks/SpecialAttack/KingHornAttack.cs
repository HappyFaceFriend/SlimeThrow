using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingHornAttack : MeleeAttack
{
    protected override IEnumerator AttackCoroutine()
    {
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        Vector3 targetPos = (_targetPos - transform.position).normalized * Slime.AttackRange.Value + originalPos;
        while (eTime < Duration)
        {
            eTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPos, targetPos, _normPosOfAttack);
            yield return null;
        }
        if (_willReturn)
            transform.position = originalPos;
        IsAttackDone = true;
    }
}
