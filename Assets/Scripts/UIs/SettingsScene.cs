using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsScene : MonoBehaviour
{
    [SerializeField] Scrollbar volumeSlider;
    [SerializeField] Image koButtonPressed;
    [SerializeField] Image koButtonNotPressed;
    [SerializeField] Image enButtonPressed;
    [SerializeField] Image enButtonNotPressed;

    [SerializeField] Panel _mainPanel;

    public void ExitScene()
    {
        StartCoroutine(ClosePanel());
    }
    IEnumerator ClosePanel()
    {
        _mainPanel.Close();
        while (_mainPanel.gameObject.activeSelf)
            yield return null;
        SceneManager.UnloadSceneAsync("SettingsScene");
    }
    private void Start()
    {
        volumeSlider.value = SaveDataManager.Instance.Volume;
        SetLanguageAs(SaveDataManager.Instance.Language);
    }
    public void OnVolumeSet()
    {
        SaveDataManager.Instance.Volume = volumeSlider.value;
    }
    public void OnLanguageButtonPressed(string code)
    {
        SetLanguageAs(code);
        SaveDataManager.Instance.Language = code;
    }
    
    void SetLanguageAs(string code)
    {
        if (code == "ko")
        {
            koButtonPressed.gameObject.SetActive(true);
            koButtonNotPressed.gameObject.SetActive(false);
            enButtonPressed.gameObject.SetActive(false);
            enButtonNotPressed.gameObject.SetActive(true);
        }
        else
        {
            koButtonPressed.gameObject.SetActive(false);
            koButtonNotPressed.gameObject.SetActive(true);
            enButtonPressed.gameObject.SetActive(true);
            enButtonNotPressed.gameObject.SetActive(false);
        }
    }
}
