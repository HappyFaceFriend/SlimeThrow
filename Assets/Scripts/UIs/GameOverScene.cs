using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] string _titleSceneName;

    public void OpenTitleScene()
    {
        SceneManager.LoadScene(_titleSceneName);
    }
}
