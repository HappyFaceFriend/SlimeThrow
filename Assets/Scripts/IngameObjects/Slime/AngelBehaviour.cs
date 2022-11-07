using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelBehaviour : SlimeBehaviour
{
    GameObject _currentPlayer;
    public float _recoverHp;
    //GameObject _recoveryPrefab;
    protected override void Awake()
    {
        base.Awake();
        _currentPlayer = GlobalRefs.Player.gameObject;
        _camera = Camera.main.GetComponent<CameraController>();
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
        _currentPlayer.GetComponent<PlayerBehaviour>().HpSystem.ChangeHp(_recoverHp);
        //Instantiate(_recoveryPrefab, _currentPlayer.transform.position, Quaternion.identity);
    }
}
