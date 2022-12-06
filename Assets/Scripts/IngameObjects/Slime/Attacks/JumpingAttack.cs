using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class JumpingAttack : SlimeAttackBase
{
    [SerializeField] SlimeJumptile _jumpTile;
    [SerializeField] GameObject _warningPrefab;
    GameObject _instantiated;
    Transform _target;

    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
        GameObject _instantiated = Instantiate(_warningPrefab, transform.position, Quaternion.identity);
        Destroy(_instantiated.gameObject, 0.35f);
    }
    public void AnimEvent_JumpingAttack()
    {
        SlimeJumptile jumptile = Instantiate(_jumpTile, transform.position, Quaternion.identity);
        jumptile.Init(_target.position, Slime);
        Destroy(jumptile.gameObject, 0.35f);
        
    }

    protected override IEnumerator AttackCoroutine()
    {
        float eTime = 0f;
        while (eTime < Duration)
        {
            eTime += Time.deltaTime;
            yield return null;
        }
        IsAttackDone = true;
    }
}
