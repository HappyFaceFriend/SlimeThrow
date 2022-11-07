using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] string _gameSceneName;
    public void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void LoadGame()
    {
        Debug.Log("로딩 미구현");
    }
}
