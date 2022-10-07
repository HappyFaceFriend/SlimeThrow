using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackBase : MonoBehaviour
{
    Coroutine _attackCoroutine;
    protected SlimeBehaviour Slime { get; private set; }
    bool _isAttackDone;

    public bool IsAttackDone { get { return _isAttackDone; } protected set { _isAttackDone = value; } }
    
    private void Awake()
    {
        Slime = GetComponent<SlimeBehaviour>();
    }

    public virtual Transform GetAttackableTarget()
    {
        if(Utils.Vectors.IsInDistance(GlobalRefs.Flower.transform.position, transform.position, Slime.AttackRange))
        {
            return GlobalRefs.Flower.transform;
        }
        if (Utils.Vectors.IsInDistance(GlobalRefs.Player.transform.position, transform.position, Slime.AttackRange))
        {
            return GlobalRefs.Player.transform;
        }
        return null;
    }

    public virtual void StartAttack(Transform targetTransform)
    {
        OnStartAttack(targetTransform);
        _attackCoroutine = StartCoroutine(AttackCoroutine());
        _isAttackDone = false;
    }
    protected virtual void OnStartAttack(Transform targetTransform)
    {
    }

    public void StopAttack()
    {
        StopCoroutine(_attackCoroutine);
        _isAttackDone = false;
    }


    protected virtual IEnumerator AttackCoroutine()
    {
        yield return null;
    }
}
