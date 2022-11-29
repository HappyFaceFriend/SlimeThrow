using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackBase : MonoBehaviour
{
    [SerializeField] float _duration;
    protected float Duration { get { return _duration; } }
    Coroutine _attackCoroutine;
    protected SlimeBehaviour Slime { get; private set; }
    bool _isAttackDone;
    public bool IsAttackDone { get { return _isAttackDone; } protected set { _isAttackDone = value; } }
    Utils.Timer _coolDownTimer;
    public bool IsCoolDownReady { get { return _coolDownTimer.IsOver; } }

    Vector3 _targetFlowerPos;
    protected void Awake()
    {
        Slime = GetComponent<SlimeBehaviour>();
        _coolDownTimer = new Utils.Timer(0f, true);
    }
    public virtual Transform GetAttackableTarget()
    {
        if(Utils.Vectors.IsInDistance(GlobalRefs.Flower.transform.position, transform.position, Slime.FlowerAttackRange))
        {
            return GlobalRefs.Flower.transform;
        }
        if (Utils.Vectors.IsInDistance(GlobalRefs.Player.transform.position, transform.position, Slime.AttackRange.Value))
        {
            if(GlobalRefs.Player.IsTargetable)
                return GlobalRefs.Player.transform;
        }
        return null;
    }

    public void StartFlowerAttack(Transform flowerTranform)
    {
        _targetFlowerPos = flowerTranform.position;
        _attackCoroutine = StartCoroutine(FlowerAttackCoroutine());
        _isAttackDone = false;
    }


    IEnumerator FlowerAttackCoroutine()
    {
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        Vector3 targetPos = (_targetFlowerPos - transform.position).normalized * Slime.AttackRange.Value + originalPos;
        while (eTime < Duration)
        {
            eTime += Time.deltaTime;
            transform.position = Vector3.Lerp(originalPos, targetPos, Slime.NormPosOfFlowerAttack);
            yield return null;
        }
        transform.position = originalPos;
        IsAttackDone = true;
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
        _isAttackDone = true;
        _coolDownTimer.Reset(1f / Slime.AttackSpeed.Value);
    }

    private void Update()
    {
        if(_isAttackDone)
            _coolDownTimer.Tick();
    }


    protected virtual IEnumerator AttackCoroutine()
    {
        yield return null;
    }
}
