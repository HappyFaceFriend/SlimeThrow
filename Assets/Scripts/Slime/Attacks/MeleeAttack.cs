using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : SlimeAttackBase
{
    Vector3 _targetPos;

    [SerializeField] float _normPosOfAttack;
    [SerializeField] float _duration;


    public override Transform GetAttackableTarget()
    {
        if (Utils.Vectors.IsInDistance(GlobalRefs.Flower.transform.position, transform.position, Slime.AttackRange))
        {
            return GlobalRefs.Flower.transform;
        }
        if (Utils.Vectors.IsInDistance(GlobalRefs.Player.transform.position, transform.position, Slime.AttackRange))
        {
            return GlobalRefs.Player.transform;
        }
        return null;
    }

    protected override void OnStartAttack(Transform targetTransform)
    {
        _targetPos = targetTransform.position;
    }
    public void CheckCollision()
    {

    }

    protected override IEnumerator AttackCoroutine()
    {
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        Vector3 targetPos = (_targetPos - transform.position).normalized * Slime.AttackRange + originalPos;
        while (eTime < _duration)
        {
            eTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPos, targetPos, _normPosOfAttack);
            yield return null;
        }
        transform.position = originalPos;
        IsAttackDone = true;
    }
}
