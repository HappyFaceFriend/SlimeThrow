using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class PausedPanel : Panel
{
    [SerializeField] TextMeshProUGUI StageText;

    private void OnEnable()
    {
        StageText.text = GlobalRefs.LevelManger.CurrentStage.ToString();
    }
}
