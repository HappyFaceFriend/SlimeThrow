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
    int _slimeKilled;

    public int SlimeKilled { get { return _slimeKilled; } set { _slimeKilled = value; } }
    public SlimeHerdSpawner Spawner { get { return _spawner; } }
    public bool IsLastEffect { get; private set; } = false;

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
        _slimeKilled = 0;
        InitFlower();
        if(GlobalDataManager.Instance.Load)
            //LoadGame();
        _stagePanel.SetStage(_currentStage);
        //Loop
        while (_currentStage < _spawner.MaxStage)
        {
            SaveData data = new SaveData(0, _currentStage, GlobalRefs.UpgradeManager.UpgradesNames, (double)GlobalRefs.Player.HpSystem.CurrentHp, (double)GlobalRefs.Flower.HPSystem.CurrentHp, SaveDataManager.Instance.GetLanguage, (double)SaveDataManager.Instance.GetVolume);
            SaveDataManager.Instance.Save(data);

            yield return _stagePanel.StartNewStage(_currentStage);
            yield return new WaitForSeconds(1f);
            _spawner.StartStage(_currentStage);

            while (!(!_spawner.IsSpawning && _spawner.LeftSlimes == 0))
                yield return null;
            foreach(SlimeBehaviour slime in _spawner.RecentDead)
            {
                slime.IsLastSlimeToDie = true;
            }
            SoundManager.Instance.PlaySFX("LastSlimeDead");
            yield return Camera.main.GetComponent<CameraController>().LastHitCoroutine();

            yield return new WaitForSeconds(1f);
            GlobalRefs.Player.EverythingStopped = true;
            yield return new WaitForSeconds(_timeAfterStageClear);

            //업그레이드
            ResetPlayer();
            yield return GlobalRefs.UpgradeManager.SelectUpgrade(_currentStage);

            GlobalRefs.Player.EverythingStopped = false;
            _currentStage++;
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
        data.SlimeKilled = _slimeKilled;
        data.Upgrades = new UpgradeData[GlobalRefs.UpgradeManager.SelectedUpgrades.Count];
        for (int i = 0; i < data.Upgrades.Length; i++)
            data.Upgrades[i] = GlobalRefs.UpgradeManager.SelectedUpgrades[i];

        GameOverDataManager.Data = data;

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
            GlobalRefs.Player.HpSystem.ChangeHp((float)data._playerHP - GlobalRefs.Player.MaxHp.Value);
            GlobalRefs.Player.PlayerHPBar.SetHp((int)GlobalRefs.Player.HpSystem.CurrentHp, (int)GlobalRefs.Player.HpSystem.MaxHp.Value);
            GlobalRefs.Flower.HPSystem.ChangeHp((float)data._flowerHP - GlobalRefs.Flower.HPSystem.MaxHp.Value);
            GlobalRefs.Flower.HPBar.SetHp((int)GlobalRefs.Flower.HPSystem.CurrentHp, (int)GlobalRefs.Flower.HPSystem.MaxHp.Value);
            SaveDataManager.Instance.Language = data._language;
            SaveDataManager.Instance.Volume = (float)data._volume;
        }
    }
}
