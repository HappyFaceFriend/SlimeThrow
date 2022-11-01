using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class JumpingAttack : SlimeAttackBase
{
    [SerializeField] SlimeJumptile _jumpTile;
    Transform _target;

    protected override void OnStartAttack(Transform targetTransform)
    {
        _target = targetTransform;
    }
    public void AnimEvent_JumpingAttack()
    {
        SlimeJumptile jumptile = Instantiate(_jumpTile, transform.position, Quaternion.identity);
        jumptile.Init(_target.position, Slime);
        Destroy(jumptile, 3f);
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
