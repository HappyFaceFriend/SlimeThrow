using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : SlimeAttackBase
{
    Vector3 _targetPos;

    [SerializeField] float _normPosOfAttack;

    Collider2D[] _hitBoxColliders;
    Vector3 _posBeforeAttack;
    new protected void Awake()
    {
        base.Awake();
        _posBeforeAttack = transform.position;
    }

    protected override void OnStartAttack(Transform targetTransform)
    {
        _targetPos = targetTransform.position;
    }

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
        transform.position = originalPos;
        IsAttackDone = true;
    }
}
