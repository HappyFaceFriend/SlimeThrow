using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{

    static bool _isFirst = true;
    [SerializeField] string _gameSceneName;
    [SerializeField] Animator _animator;
    [SerializeField] float _cameraMoveMagnitude;

    bool _canCameraMove = false;

    private void Awake()
    {
        if (_isFirst)
            _isFirst = false;
        else
            _animator.SetTrigger("SkipEnter");
    }
    public void NewGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void LoadGame()
    {
        Debug.Log("로딩 미구현");
        SceneManager.LoadScene("TitleScene");
    }
    public void Settings()
    {
        Debug.Log("로딩 미구현");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void EnableCameraMove()
    {
        if(_canCameraMove == false)
            SoundManager.Instance.PlayBGM("MainBGM");
        _canCameraMove = true;
    }

    private void Update()
    {
        if (!_canCameraMove)
            return;
        Vector3 offset = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0)) * _cameraMoveMagnitude;
        Camera.main.transform.position = new Vector3(offset.x, offset.y, Camera.main.transform.position.z);
    }
}
