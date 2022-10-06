using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackBase : MonoBehaviour
{
    Vector3 _targetPos;
    [Tooltip("x : 시간 (0~1), y : 0이 현재위치, 1이 target위치")]
    [SerializeField] AnimationCurve _attackCurve;
    [SerializeField] float _duration;

    Coroutine _attackCoroutine;
    SlimeBehaviour _slime;
    bool _isAttackDone;

    public bool IsAttackDone { get { return _isAttackDone; } }
    private void Awake()
    {
        _slime = GetComponent<SlimeBehaviour>();
    }

    public Transform GetAttackableTarget()
    {
        if(Utils.Vectors.IsInDistance(GlobalRefs.Flower.transform.position, transform.position, _slime.AttackRange))
        {
            return GlobalRefs.Flower.transform;
        }
        if (Utils.Vectors.IsInDistance(GlobalRefs.Player.transform.position, transform.position, _slime.AttackRange))
        {
            return GlobalRefs.Player.transform;
        }
        return null;
    }

    public void StartAttack(Transform targetTransform)
    {
        _targetPos = targetTransform.position;
        _attackCoroutine = StartCoroutine(AttackCoroutine());
        _isAttackDone = false;
    }

    public void StopAttack()
    {
        StopCoroutine(_attackCoroutine);
        _isAttackDone = false;
    }

    public void CheckCollision()
    {

    }

    IEnumerator AttackCoroutine()
    {
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        Vector3 targetPos = (_targetPos - transform.position).normalized * _slime.AttackRange + originalPos;
        while(eTime < _duration)
        {
            eTime += Time.deltaTime;
            float process = Mathf.Clamp01(eTime / _duration);
            float curveValue = _attackCurve.Evaluate(process);
            transform.position = Vector3.Lerp(originalPos, targetPos, curveValue);
            yield return null;
        }
        transform.position = originalPos;
        _isAttackDone = true;
    }
}
