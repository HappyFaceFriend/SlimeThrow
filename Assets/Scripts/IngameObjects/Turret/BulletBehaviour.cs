using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] public float _moveSpeed;
    [SerializeField] public float _rotationAmount;
    [SerializeField] float _turretHeight;
    [SerializeField] float _maxZHeight;

    [SerializeField] LandEffectBehaviour _landEffectPrefab;

    [Header("Images")]
    [SerializeField] SpriteRenderer _one;
    [SerializeField] SpriteRenderer [] _two;
    [SerializeField] SpriteRenderer [] _three;
    Vector3 _targetPosition;
    PlayerBehaviour _player;

    List<LandEffectInfo> _landEffectInfos;
    public void StartShoot(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        StartCoroutine(MoveCoroutine());
    }
    public void ApplyEffects(List<LandEffectInfo> landEffects, PlayerBehaviour player, List<Color> colors)
    {
        _landEffectInfos = landEffects;
        _player = player;

        _one.gameObject.SetActive(colors.Count == 1);
        foreach (var sprite in _two)
            sprite.gameObject.SetActive(colors.Count == 2);
        foreach (var sprite in _three)
            sprite.gameObject.SetActive(colors.Count == 3);

        for (int i = 0; i < colors.Count; i++)
            colors[i] *= 0.8f;

        if (colors.Count == 1)
            _one.color = colors[0];
        else if (colors.Count == 2)
        {
            _two[0].color = colors[0];
            _two[1].color = colors[1];
        }
        else
        {
            _three[0].color = colors[0];
            _three[1].color = colors[1];
            _three[2].color = colors[2];
        }
    }

    void OnLand()
    {
        LandEffectBehaviour landEffect = Instantiate(_landEffectPrefab);
        landEffect.transform.position = transform.position;
        landEffect.ApplyEffects(new List<LandEffectInfo>(_landEffectInfos), _player != null ? _player.DamageAsBullet.Value : 0);
        if(_player != null)
        {
            _player.LandWithBullet(transform.position);
        }
        SoundManager.Instance.PlaySFX("BulletLand1", 1.3f);
        SoundManager.Instance.PlaySFX("BulletLand2", 1.3f);
        Destroy(gameObject);
    }
    IEnumerator MoveCoroutine()
    {
        float z = _turretHeight;
        Vector3 originalShadowPos = transform.position - new Vector3(0, _turretHeight, 0);

        float totalDistance = (_targetPosition - originalShadowPos).magnitude;
        float movedDistance = 0;
        Vector3 moveDir = (_targetPosition - originalShadowPos) / totalDistance;

        Vector3 lastShadowPos = originalShadowPos;

        float rotDirection = (_targetPosition.x - originalShadowPos.x) > 0 ? -1 : 1;

        while(true)
        {
            Vector3 shadowPos = lastShadowPos + moveDir * _moveSpeed * Time.deltaTime;
            movedDistance += _moveSpeed * Time.deltaTime;
            if (movedDistance <= totalDistance / 2f)
                z = Mathf.Lerp(_turretHeight, _maxZHeight, 
                    Utils.Curves.EaseOut(movedDistance / (totalDistance / 2)));
            else
                z = Mathf.Lerp(_maxZHeight, 0, 
                    Utils.Curves.EaseIn((movedDistance - totalDistance / 2) / (totalDistance / 2)));

            transform.position = shadowPos + new Vector3(0, z, 0);
            if (Utils.Vectors.IsPositionCrossed(_targetPosition, shadowPos, lastShadowPos))
                break;
            float rotation = Mathf.Lerp(0, _rotationAmount * rotDirection, movedDistance / totalDistance);
            transform.rotation = Quaternion.Euler(0, 0, rotation);
            lastShadowPos = shadowPos;
            yield return null;
        }
        //Æø¹ß
        OnLand();
    }
}
