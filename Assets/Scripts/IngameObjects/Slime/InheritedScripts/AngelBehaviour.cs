using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBehaviour : SlimeBehaviour
{
    [SerializeField] PlayerBehaviour _currentPlayer;
    public float _recoverHp;
    //GameObject _recoveryPrefab;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnDestroy()
    {
        if (PuttedInTurret)
            return;
        _camera.Shake(CameraController.ShakePower.SlimeHitted);
        float smokeSpeed = 0.7f;
        float angleOffset = Random.Range(-15, 15);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(90 + angleOffset) * smokeSpeed);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(225 + angleOffset) * smokeSpeed);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(315 + angleOffset) * smokeSpeed);
        _currentPlayer.RecoveHP(_recoverHp);
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
