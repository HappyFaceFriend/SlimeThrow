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

    [Header("Anim Settings")]
    [SerializeField] float _downDuration;
    [SerializeField] float _stayDuration;
    [SerializeField] float _upDuration;
    [SerializeField] float _targetScale;
    [SerializeField] Animator _stageTextPanel;
    [SerializeField] Transform _stageTextTarget;


    Vector3 _stageTextOriginalPos;
    RectTransform _layout;

    int _currentIndex;

    private void Awake()
    {
        _layout = GetComponent<RectTransform>();
    }
    private void Start()
    {
        _stageTextOriginalPos = _stageTextPanel.transform.position;
    }

    void SetTextPosition(float y)
    {
        _stageTextPanel.transform.position = new Vector3(_stageTextPanel.transform.position.x, y, _stageTextPanel.transform.position.z);
    }
    public IEnumerator StartNewStage(int stage)
    {
        SetStage(stage);
        Vector3 center = _stageTextTarget.transform.position;
        StartCoroutine(Utils.Lerp.EaseCoroutine(x => _stageTextPanel.transform.localScale = new Vector3(x,x,1),1, _targetScale, _downDuration));
        yield return Utils.Lerp.EaseCoroutine(y => SetTextPosition(y), _stageTextOriginalPos.y, center.y, _downDuration);
        _stageTextPanel.SetTrigger("Shake");
        yield return new WaitForSeconds(_stayDuration);
        //위로 올리기
        StartCoroutine(Utils.Lerp.EaseCoroutine(x => _stageTextPanel.transform.localScale = new Vector3(x, x, 1), _targetScale, 1, _downDuration));
        yield return Utils.Lerp.EaseCoroutine(y => SetTextPosition(y), center.y, _stageTextOriginalPos.y, _downDuration);
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
