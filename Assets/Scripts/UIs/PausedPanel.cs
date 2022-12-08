using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class PausedPanel : Panel
{
    [SerializeField] TextMeshProUGUI StageText;

    protected void OnEnable()
    {
        base.OnEnable();
        StageText.text = GlobalRefs.LevelManger.CurrentStage.ToString();
        SoundManager.Instance.PlaySFX("ButtonClick");
    }
}
