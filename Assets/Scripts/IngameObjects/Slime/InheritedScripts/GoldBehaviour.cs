using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBehaviour : SlimeBehaviour
{
    [SerializeField] GameObject _item;
    protected override void Awake()
    {
        base.Awake();
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
            return;
        if (PuttedInTurret)
            return;
        _camera.Shake(CameraController.ShakePower.SlimeHitted);
        float smokeSpeed = 0.7f;
        float angleOffset = Random.Range(-15, 15);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(90 + angleOffset) * smokeSpeed);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(225 + angleOffset) * smokeSpeed);
        EffectManager.InstantiateSmokeEffect(transform.position, Utils.Vectors.AngleToVector(315 + angleOffset) * smokeSpeed);
        Instantiate(_item, transform.position, Quaternion.identity);
    }
}
