using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBehaviour : SlimeBehaviour
{
    public float _recoverHp;
    //GameObject _recoveryPrefab;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnDestroy()
    {
        base.OnDestroy();
        GlobalRefs.Player.RecoveHP(_recoverHp);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsAlive)
            return;
        var target = collision.collider.GetComponent<PlayerBehaviour>();
        if (target != null)
        {
            target.OnHittedBySlime(this, 0);
        }
    }
}
