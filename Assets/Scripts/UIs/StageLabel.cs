using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLabel : MonoBehaviour
{
    public enum Type { Current, Boss, Later }
    [SerializeField] Image _slimeIcon;
    [SerializeField] float _scaleAtCurrent;
    [SerializeField] float _scaleAtBoss;
    [SerializeField] float _scaleAtLater;

    RectTransform _rectTransform;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = new Vector3(0, 0, 1);
    }

    public void Init(Type type, Sprite slimeSprite = null)
    {
        _slimeIcon.sprite = slimeSprite;
        if (type == Type.Later)
            StartCoroutine(Utils.Lerp.EaseCoroutine(x => _rectTransform.localScale = new Vector3(x, x, 1),
                0, _scaleAtLater, 0.5f));
        else if (type == Type.Current)
            StartCoroutine(Utils.Lerp.EaseCoroutine(x => _rectTransform.localScale = new Vector3(x, x, 1),
                0, _scaleAtCurrent, 0.5f));
        else
            StartCoroutine(Utils.Lerp.EaseCoroutine(x => _rectTransform.localScale = new Vector3(x, x, 1),
                0, _scaleAtBoss, 0.5f));
    }
    public void SetToCurrent()
    {
        StartCoroutine(Utils.Lerp.EaseCoroutine(x => _rectTransform.localScale = new Vector3(x, x, 1),
            _rectTransform.localScale.x, _scaleAtCurrent, 0.5f));
    }
    public void Remove()
    {
        StartCoroutine(Utils.Lerp.EaseCoroutine(x => _rectTransform.localScale = new Vector3(x, x, 1),
            _rectTransform.localScale.x, 0, 0.5f));
        Destroy(gameObject, 0.51f);
    }

}
