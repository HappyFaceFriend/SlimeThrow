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

    Vector3 _shakeOffset= Vector3.zero;
    Vector3 _focusPosition = Vector3.zero;
    Vector3 _targetFocus = Vector3.zero;
    float _targetSize;

    Coroutine _currentShake;
    Coroutine _currentZoom;
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
                _shakeOffset = Vector3.zero;
            }
            else
                return;
        }
        if (power == ShakePower.SlimeHitted)
            StartCoroutine(ShakeCoroutine(_slimeHitted));
        else if (power == ShakePower.BulletLanded)
            StartCoroutine(ShakeCoroutine(_bulletLanded));
        else if (power == ShakePower.SlimeLastHitted)
            StartCoroutine(ShakeCoroutine(_slimeLastHitted));
        _currentPower = power;
    }

    public void Zoom(Vector3 focusPosition, float zoom)
    {
        _targetFocus = focusPosition;
        _targetSize = zoom * _originalSize;
        if (_currentZoom != null)
            StopCoroutine(_currentZoom);
        _currentZoom = StartCoroutine(ZoomCoroutine(0.5f));
    }
    IEnumerator ZoomCoroutine(float duration)
    {
        float eTime = 0f;
        Vector3 originalFocus = _focusPosition;
        float originalSize = _camera.orthographicSize;
        while(eTime < duration)
        {
            eTime += Time.unscaledDeltaTime;
            _focusPosition = Vector3.Lerp(originalFocus, _targetFocus, Utils.Curves.EaseOut(eTime / duration));
            _camera.orthographicSize = Mathf.Lerp(originalSize, _targetSize, Utils.Curves.EaseOut(eTime / duration));
            yield return null;
        }
        _focusPosition = _targetFocus;
        _camera.orthographicSize = _targetSize;
    }

    delegate float Curve(float t);
    IEnumerator ShakeCoroutine(CameraShakeParams shakeParams)
    {
        float eTime = 0f;
        Curve curve;
        if (shakeParams.Curve == CameraShakeParams.CurveType.EaseIn)
            curve = Utils.Curves.EaseIn;
        else if (shakeParams.Curve == CameraShakeParams.CurveType.Linear)
            curve = Utils.Curves.Linear;
        else
            curve = Utils.Curves.Constant;
        while (eTime < shakeParams.Duration)
        {
            eTime += Time.unscaledDeltaTime;
            _shakeOffset = Utils.Random.RandomUnitVector2() * shakeParams.Magnitude * curve(eTime / shakeParams.Duration);
            yield return null;
        }
        _shakeOffset = Vector3.zero;
    }

    public void Update()
    {
        transform.position = _originalPosition + _focusPosition + _shakeOffset;
    }
}
