using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class StagePanel : MonoBehaviour
{
    List<StageLabel> _stageLabels = new List<StageLabel>();

    [SerializeField] SlimeHerdSpawner _spawner;
    [SerializeField] TextMeshProUGUI _stageText;
    [SerializeField] TextMeshProUGUI _stageText2;
    [SerializeField] StageLabel _stageLabelPrefab;
    [SerializeField] int _showCount;
    [SerializeField] bool _noIcons;

    RectTransform _layout;

    int _currentIndex;

    private void Awake()
    {
        _layout = GetComponent<RectTransform>();
    }
    public void SetStage(int stage)
    {
        _stageText.text = "Stage " + (stage + 1);
        _stageText2.text = "Stage " + (stage + 1);
    }/*
    public void Init(int offset)
    {
        _currentIndex = offset;
        SetStage(_currentIndex);
        if (_noIcons)
            return;
        for (int i=0; i< _showCount && i<_spawner.MaxStage; i++)
        {
            StageLabel label = Instantiate(_stageLabelPrefab, transform);
            if(i == 0)
                label.Init(StageLabel.Type.Current, _spawner.LabelImages[_currentIndex+i]);
            else
                label.Init(_spawner.LabelTypes[i], _spawner.LabelImages[_currentIndex+i]);
            _stageLabels.Add(label);
        }
    }

    private void LateUpdate()
    {

        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);
    }
    public void SetToNextStage()
    {
        if (!_noIcons)
        {
            _stageLabels[0].Remove();
            _stageLabels.RemoveAt(0);
        }


        _currentIndex++;
        if (_currentIndex + _showCount < _spawner.MaxStage)
        {
            SetStage(_currentIndex);
            if (_noIcons)
                return;
            _stageLabels[0].SetToCurrent();
            StageLabel label = Instantiate(_stageLabelPrefab, transform);
            label.Init(_spawner.LabelTypes[_currentIndex + _showCount], _spawner.LabelImages[_currentIndex + _showCount]);
            _stageLabels.Add(label);
        }
    }*/
}
