using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using LitJson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class LevelManager : MonoBehaviour
{
    public Vector2 MapSize { get { return _mapSize; } }
    [SerializeField] Vector2 _mapSize;
    [SerializeField] string _gameOverSceneName;
    [SerializeField] Flower _flower;
    [SerializeField] SlimeHerdSpawner _spawner;
    [SerializeField] List<FlowerPlantPoint> _dirts;
    [SerializeField] float _timeAfterStageClear;

    [SerializeField] StagePanel _stagePanel;
    List<string> _upgradeList;

    int _currentStage;
    public SlimeHerdSpawner Spawner { get { return _spawner; } }

    private void Start()
    {
        if (_spawner != null)
        {
            StartCoroutine(GameLoop());

            SoundManager.Instance.PlayBGM("Game");
        }
    }

    void InitFlower()
    {
        int dirtIdx = Random.Range(0, _dirts.Count);
        _dirts[dirtIdx].PlantFlower(_flower);
    }
    IEnumerator GameLoop()
    {
        //Init
        _currentStage = 0;
        InitFlower();
        //LoadGame();
        _stagePanel.SetStage(_currentStage);
        //Loop
        while (_currentStage < _spawner.MaxStage)
        {
            SaveData data = new SaveData(0, _currentStage, GlobalRefs.UpgradeManager.UpgradesNames, (double)GlobalRefs.Player.HpSystem.CurrentHp, (double)GlobalRefs.Flower.HPSystem.CurrentHp);
            SaveDataManager.Instance.Save(data);


            _spawner.StartStage(_currentStage);

            while (_spawner.IsSpawning)
                yield return null;

            while (_spawner.LeftSlimes > 0)
                yield return null;
            SoundManager.Instance.PlaySFX("LastSlimeDead");
            Camera.main.GetComponent<CameraController>().StartLastHitEffect();

            yield return new WaitForSeconds(_timeAfterStageClear);

            //업그레이드
            GlobalRefs.Player.EverythingStopped = true;
            ResetPlayer();
            yield return GlobalRefs.UpgradeManager.SelectUpgrade(_currentStage);

            GlobalRefs.Player.EverythingStopped = false;
            _currentStage++;
            _stagePanel.SetStage(_currentStage);
        }
    }

    void ResetPlayer()
    {
        if (GlobalRefs.Player.CurrentState is PlayerStates.InTurretState)
        {
            GlobalRefs.Player.ForceOutOfTurret();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            OnPlayerDead();
    }
    public void OnPlayerDead()
    {
        OpenGameOver();
    }
    public void OnFlowerDead()
    {
        OpenGameOver();
    }
    void OpenGameOver()
    {
        SoundManager.Instance.PlaySFX("GameOver");
        GameOverDataManager.GameOverData data = new GameOverDataManager.GameOverData();
        data.Stage = _currentStage;

        GameOverDataManager.SetData(data);

        SceneManager.LoadScene(_gameOverSceneName, LoadSceneMode.Additive);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        Gizmos.DrawWireCube(new Vector3(0, 0, 0.01f), new Vector3(_mapSize.x, _mapSize.y, 0.01f));
    }
    void LoadGame()
    {
        SaveData data = SaveDataManager.Instance.Load();
        if (data != null)
        { 
                _currentStage = data._stage;
                GlobalRefs.UpgradeManager.FindUpgrade(data._upgrades);
                GlobalRefs.Player.HpSystem.ChangeHp(GlobalRefs.Player.MaxHp.Value - (float)data._playerHP);
                GlobalRefs.Flower.HPSystem.ChangeHp(GlobalRefs.Flower.HPSystem.MaxHp.Value - (float)data._flowerHP);
           
        }
    }
}
