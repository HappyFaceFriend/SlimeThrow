using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : SlimeAttackBase
{
    Vector3 _targetPos;

    [SerializeField] float _normPosOfAttack;
    [SerializeField] GameObject _hitBoxObject;

    Collider2D[] _hitBoxColliders;

    protected void Awake()
    {
        base.Awake();
        _hitBoxColliders = _hitBoxObject.GetComponents<Collider2D>();
        
    }
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
    public void AnimEvent_CheckCollision()
    {
        List<IAttackableBySlime> hittedList = new List<IAttackableBySlime>();
        foreach (Collider2D collider in _hitBoxColliders)
        {
            ContactFilter2D filter = new ContactFilter2D();
            List<Collider2D> results = new List<Collider2D>();
            collider.OverlapCollider(filter.NoFilter(), results);

            foreach (Collider2D c in results)
            {
                IAttackableBySlime result = c.GetComponent<IAttackableBySlime>();
                if (result != null && !hittedList.Contains(result))
                {
                    hittedList.Add(result);
                }
            }
        }
        foreach(IAttackableBySlime hitted in hittedList)
        {
            hitted.OnHittedBySlime(Slime, Slime.AttackPower);
        }
    }

    protected override IEnumerator AttackCoroutine()
    {
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        Vector3 targetPos = (_targetPos - transform.position).normalized * Slime.AttackRange + originalPos;
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
