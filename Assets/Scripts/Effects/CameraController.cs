using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum ShakePower { SlimeHitted, BulletLanded, SlimeLastHitted }
    [Header("Camera Shakes")]
    [SerializeField] CameraShakeParams _slimeHitted;
    [SerializeField] CameraShakeParams _bulletLanded;
    [SerializeField] CameraShakeParams _slimeLastHitted;

    [Header("Other Params")]
    [SerializeField] float _followSpeed;
    [SerializeField] Transform _followTarget;

    Vector3 _originalPosition;

    Vector3 _shakeOffset= Vector3.zero;
    Vector3 _focusPosition = Vector3.zero;
    Vector3 _targetFocus = Vector3.zero;

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
        float speed = (_focusPosition - _followTarget.position).magnitude * _followSpeed;
        if (speed < 0.1f)
            speed = 0f;
        _focusPosition = Vector3.MoveTowards(_focusPosition, _followTarget.position,
                     speed  * Time.deltaTime);
        transform.position = _originalPosition + _focusPosition + _shakeOffset;
    }
}
