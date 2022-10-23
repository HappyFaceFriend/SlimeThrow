using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum ShakePower { SlimeHitted, BulletLanded, SlimeLastHitted }
    [SerializeField] CameraShakeParams _slimeHitted;
    [SerializeField] CameraShakeParams _bulletLanded;
    [SerializeField] CameraShakeParams _slimeLastHitted;
    float _originalSize;
    Vector3 _originalPosition;

    Vector3 _focusPosition = Vector3.zero;
    Coroutine _currentShake;
    ShakePower _currentPower;
    Camera _camera;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _originalPosition = transform.position;
    }
    private void Start()
    {
        _originalSize = _camera.orthographicSize;
    }

    public void Shake(ShakePower power)
    {
        if (_currentShake != null)
        {
            if(_currentPower <= power)
            {
                StopCoroutine(_currentShake);
                transform.position = _originalPosition;
            }
            else
            {
                return;
            }
        }
        if (power == ShakePower.SlimeHitted)
            StartCoroutine(ShakeCoroutine(_slimeHitted));
        else if (power == ShakePower.BulletLanded)
            StartCoroutine(ShakeCoroutine(_bulletLanded));
        else if (power == ShakePower.SlimeLastHitted)
            StartCoroutine(ShakeCoroutine(_slimeLastHitted));
        _currentPower = power;
    }

    delegate float Curve(float t);
    IEnumerator ShakeCoroutine(CameraShakeParams shakeParams)
    {
        float eTime = 0f;
        Vector3 offset = Vector3.zero;
        Curve curve;
        if (shakeParams.Curve == CameraShakeParams.CurveType.EaseIn)
            curve = Utils.Curves.EaseIn;
        else if (shakeParams.Curve == CameraShakeParams.CurveType.Linear)
            curve = Utils.Curves.Linear;
        else
            curve = Utils.Curves.Constant;
        while (eTime < shakeParams.Duration)
        {
            eTime += Time.deltaTime;
            offset = Utils.Random.RandomUnitVector2() * shakeParams.Magnitude * curve(eTime / shakeParams.Duration);
            transform.position = _originalPosition + _focusPosition + offset;
            yield return null;
        }
        transform.position = _originalPosition + _focusPosition;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            Shake(ShakePower.SlimeHitted);
    }
}
