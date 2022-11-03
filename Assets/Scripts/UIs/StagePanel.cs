using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePanel : MonoBehaviour
{
    List<StageLabel> _stageLabels = new List<StageLabel>();

    [SerializeField] SlimeSpawner _spawner;
    [SerializeField] StageLabel _stageLabelPrefab;
    [SerializeField] int _showCount;

    RectTransform _layout;

    int _currentIndex;

    private void Awake()
    {
        _layout = GetComponent<RectTransform>();
    }
    public void Init()
    {
        _currentIndex = 0;
        for(int i=0; i< _showCount; i++)
        {
            StageLabel label = Instantiate(_stageLabelPrefab, transform);
            if(i == 0)
                label.Init(StageLabel.Type.Current, _spawner.LabelImages[i]);
            else
                label.Init(_spawner.LabelTypes[i], _spawner.LabelImages[i]);
            _stageLabels.Add(label);
        }
    }

    private void LateUpdate()
    {

        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout);
    }

    public void SetToNextStage()
    {
        _stageLabels[0].Remove();
        _stageLabels.RemoveAt(0);

        _stageLabels[0].SetToCurrent();

        if (_currentIndex + _showCount < _spawner.LabelTypes.Length)
        {
            StageLabel label = Instantiate(_stageLabelPrefab, transform);
            label.Init(_spawner.LabelTypes[_currentIndex + _showCount], _spawner.LabelImages[_currentIndex + _showCount]);
            _stageLabels.Add(label);
        }
    }
}
