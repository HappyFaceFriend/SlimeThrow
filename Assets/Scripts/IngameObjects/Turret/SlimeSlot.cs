using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeSlot : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Sprite _nothing;

    public Sprite Icon { get { return _image.sprite; } }
    public void SetImage(Sprite icon)
    {
        if (icon == null)
            _image.sprite = _nothing;
        else
            _image.sprite = icon;
    }
}
