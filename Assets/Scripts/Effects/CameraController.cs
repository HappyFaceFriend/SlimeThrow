using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraController : MonoBehaviour
{
    public enum ShakePower { SlimeHitted, BulletLanded, SlimeLastHitted, BulletShooted }
    [Header("Camera Shakes")]
    [SerializeField] CameraShakeParams _slimeHitted;
    [SerializeField] CameraShakeParams _bulletLanded;
    [SerializeField] CameraShakeParams _slimeLastHitted;
    [SerializeField] CameraShakeParams _bulletShooted;

    [Header("Other Params")]
    [SerializeField] float _followSpeed;
    [SerializeField] Transform _followTarget;
    [SerializeField] float _minDistanceFromEdge;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] float _cameraSpeed;
    [Header("Last Hit Effect")]
    [SerializeField] float _lastHitZoomSize;
    [SerializeField] float _lastHitDuration;
    [SerializeField] float _lastHitTimeScale;
    [SerializeField] CameraShakeParams _lastHit;

    Vector3 _originalPosition;

    Vector3 _shakeOffset= Vector3.zero;
    Vector3 _focusPosition = Vector3.zero;
    Vector3 _targetFocus = Vector3.zero;

    Coroutine _currentShake;
    ShakePower _currentPower;
    Camera _camera;

    Vector3 _mapSize;

    PixelPerfectCamera _pixelPerfect;
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _originalPosition = transform.position;
        _mapSize = _levelManager.MapSize;
        _pixelPerfect = GetComponent<PixelPerfectCamera>();
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
        else if (power == ShakePower.BulletShooted)
            StartCoroutine(ShakeCoroutine(_bulletShooted));
        _currentPower = power;
    }


    delegate float Curve(float t);
    IEnumerator ShakeCoroutine(CameraShakeParams shakeParams)
    {
        float eTime = 0f;
        float interval = 0.05f;
        float intervalCounter = 0f;
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
            intervalCounter += Time.deltaTime;
            if (intervalCounter > interval)
            {
                intervalCounter -= interval;
                _shakeOffset = Utils.Random.RandomUnitVector2() * shakeParams.Magnitude * curve(eTime / shakeParams.Duration);

            }
            yield return null;
        }
        _shakeOffset = Vector3.zero;
    }

    public IEnumerator LastHitCoroutine()
    {
        _pixelPerfect.enabled = false;
        float originalSize = _camera.orthographicSize;
        _followSpeed *= 30;
        Time.timeScale = _lastHitTimeScale;
        StartCoroutine(ShakeCoroutine(_slimeLastHitted));

        float eTime = 0f;
        float zoomDuration = 0.1f;
        while(eTime <= zoomDuration)
        {
            eTime += Time.unscaledDeltaTime;
            _camera.orthographicSize = Mathf.Lerp(originalSize, _lastHitZoomSize, (eTime / zoomDuration));
        }
        _camera.orthographicSize = _lastHitZoomSize;
        yield return new WaitForSecondsRealtime(_lastHitDuration - zoomDuration);

        _camera.orthographicSize = originalSize;
        _followSpeed /= 30;
        Time.timeScale = 1f;
    }
    public void Update()
    {
        float speed = (_focusPosition - _followTarget.position).magnitude * _followSpeed;
        if (speed < 0.1f)
            speed = 0f;

        Vector3 targetPos = _followTarget.position;
        float aspect = 640.0f/360.0f;
        if(targetPos.x - _camera.orthographicSize * aspect < -_mapSize.x / 2 - _minDistanceFromEdge)
        {
            targetPos.x = -_mapSize.x / 2 - _minDistanceFromEdge + _camera.orthographicSize * aspect;
        }
        else if (targetPos.x + _camera.orthographicSize * aspect > _mapSize.x / 2 + _minDistanceFromEdge)
        {
            targetPos.x = _mapSize.x / 2 + _minDistanceFromEdge - _camera.orthographicSize * aspect;
        }
        if (targetPos.y + _camera.orthographicSize > _mapSize.y / 2 + _minDistanceFromEdge)
        {
            targetPos.y = _mapSize.y / 2 + _minDistanceFromEdge - _camera.orthographicSize;
        }
        else if (targetPos.y - _camera.orthographicSize < -_mapSize.y / 2 - _minDistanceFromEdge)
        {
            targetPos.y = -_mapSize.y / 2 - _minDistanceFromEdge + _camera.orthographicSize;
        }
        _focusPosition = Vector3.MoveTowards(_focusPosition, targetPos,
                     speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, _originalPosition + _focusPosition + _shakeOffset,
                                _cameraSpeed * Time.deltaTime);
    }
}
