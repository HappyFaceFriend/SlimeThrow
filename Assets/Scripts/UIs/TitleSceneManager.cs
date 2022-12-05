using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{

    static bool _isFirst = true;
    [SerializeField] string _gameSceneName;
    [SerializeField] string _settingSceneName;
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
        SoundManager.Instance.PlaySFX("GameStartClicked", 1f);
        SceneManager.LoadScene(_gameSceneName);
    }

    public void LoadGame()
    {
        SoundManager.Instance.PlaySFX("GameStartClicked", 1f);
        Debug.Log("로딩 미구현");
        GlobalDataManager.Instance.SetLoad();
        SceneManager.LoadScene(_gameSceneName);
        
    }
    public void Settings()
    {
        SceneManager.LoadScene(_settingSceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void EnableCameraMove()
    {
        if(_canCameraMove == false)
            SoundManager.Instance.PlayBGM("Main");
        _canCameraMove = true;
    }
    public void ExplosionSound()
    {
        SoundManager.Instance.PlaySFX("BulletLand1", 1f);
        SoundManager.Instance.PlaySFX("BulletLand2", 1f);
    }
    public void SlimeWalkSound()
    {
        SoundManager.Instance.PlaySFX("MainSlimeWalk");
    }
    private void Update()
    {
        if (!_canCameraMove)
            return;
        Vector3 offset = (Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0)) * _cameraMoveMagnitude;
        Camera.main.transform.position = new Vector3(offset.x, offset.y, Camera.main.transform.position.z);
    }
}
