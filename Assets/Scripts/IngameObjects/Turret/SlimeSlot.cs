using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeSlot : MonoBehaviour
{
    [SerializeField] Image _image;

    public void SetImage(Sprite icon)
    {
        _image.sprite = icon;
    }
}
