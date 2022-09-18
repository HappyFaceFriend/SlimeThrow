using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public delegate void LandEffect();
    public delegate void FlyEffect();

    List<LandEffect> _landEffects = new List<LandEffect>();
    List<FlyEffect> _flyEffects = new List<FlyEffect>();

    [SerializeField] float _moveSpeed;
    [SerializeField] float _turretHeight;
    [SerializeField] float _maxZHeight;
    Vector3 _targetPosition;
    public void StartShoot(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        StartCoroutine(MoveCoroutine());
    }
    public void ApplyEffects(List<LandEffect> landEffects, List<FlyEffect> flyEffects)
    {
        _landEffects = new List<LandEffect>(landEffects);
        _flyEffects = new List<FlyEffect>(flyEffects);
    }
    void OnLand()
    {
        foreach(LandEffect landEffect in _landEffects)
        {
            landEffect();
        }
    }
    IEnumerator MoveCoroutine()
    {
        float z = _turretHeight;
        Vector3 originalShadowPos = transform.position - new Vector3(0, _turretHeight, 0);

        float totalDistance = (_targetPosition - originalShadowPos).magnitude;
        float movedDistance = 0;
        Vector3 moveDir = (_targetPosition - originalShadowPos) / totalDistance;

        Vector3 lastShadowPos = originalShadowPos;

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

            lastShadowPos = shadowPos;
            yield return null;
        }
        //����
        OnLand();
        gameObject.SetActive(false);
    }
}
