using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


using TMPro;
public class GameOverScene : MonoBehaviour
{
    [SerializeField] string _titleSceneName;


    [SerializeField] TextMeshProUGUI _clearedStageText;
    [SerializeField] TextMeshProUGUI _slimeKilledText;
    [SerializeField] Transform _upgradesParent;
    [SerializeField] UpgradeIcon _upgradeIconPrefab;
    [SerializeField] float _upgradeIconSize;

    [SerializeField] Panel _mainPanel;
    private void Start()
    {
        UpdatePanel();
    }
    public void OpenTitleScene()
    {
        StartCoroutine(ClosePanel());
    }
    IEnumerator ClosePanel()
    {
        _mainPanel.Close();
        while (_mainPanel.gameObject.activeSelf)
            yield return null;
        SceneManager.LoadScene(_titleSceneName);
    }
    void UpdatePanel()
    {
        if (GameOverDataManager.Data != null)
        {
            var data = GameOverDataManager.Data;
            _clearedStageText.text = "Stage " + data.Stage.ToString();
            _slimeKilledText.text = data.SlimeKilled.ToString();
            for(int i=0; i<data.Upgrades.Length; i++)
            {
                UpgradeIcon icon = Instantiate(_upgradeIconPrefab, _upgradesParent);
                icon.Init(data.Upgrades[i]);
                icon.GetComponent<RectTransform>().sizeDelta = new Vector2(_upgradeIconSize, _upgradeIconSize);
            }
        }
    }
}
