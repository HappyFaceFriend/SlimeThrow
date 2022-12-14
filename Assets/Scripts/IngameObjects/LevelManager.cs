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

    [SerializeField] PausedPanel _pausedPanel;
    [SerializeField] StagePanel _stagePanel;

    [Header("Test")]
    [SerializeField] string state;
    [SerializeField] bool test = false;
    List<string> _upgradeList;

    float _playerMoveSpeed;
    float _playerAttackSpeed;
    int _currentStage;
    int _slimeKilled;

    public bool IsPaused { get; private set; } = false;

    public int SlimeKilled { get { return _slimeKilled; } set { _slimeKilled = value; } }
    public SlimeHerdSpawner Spawner { get { return _spawner; } }
    public bool IsLastEffect { get; private set; } = false;

    public bool IsGameOver { get; private set; } = false;
    
    public int CurrentStage { get { return _currentStage; } }

    Coroutine _gameLoop = null;
    private void Start()
    {
        if (_spawner != null)
        {
            _gameLoop = StartCoroutine(GameLoop());

            SoundManager.Instance.PlayBGM("Game");
        }
    }

    void InitFlower()
    {
        int dirtIdx = Random.Range(0, _dirts.Count);
        _dirts[dirtIdx].PlantFlower(_flower);
    }

    void GetPlyaerData()
    {
        _playerAttackSpeed = GlobalRefs.Player.AttackSpeed.Value;
        _playerMoveSpeed = GlobalRefs.Player.MoveSpeed.Value;
    }

    IEnumerator GameLoop()
    {
        //Init
        _currentStage = 0;
        _slimeKilled = 0;
        InitFlower();
        if(GlobalDataManager.Instance.Load)
            LoadGame();
        _stagePanel.SetStage(_currentStage);
        //Loop
        state = "Before loop";
        while (_currentStage < _spawner.MaxStage)
        {
            GetPlyaerData();
            SaveData data = new SaveData(_currentStage, GlobalRefs.UpgradeManager.UpgradesNames, (double)GlobalRefs.Player.HpSystem.CurrentHp, (double)GlobalRefs.Flower.HPSystem.CurrentHp, SlimeKilled, SaveDataManager.Instance.Language, (double)SaveDataManager.Instance.Volume, GlobalRefs.UpgradeManager.RerollCount);
            SaveDataManager.Instance.Save(data);

            state = "Save";
            yield return _stagePanel.StartNewStage(_currentStage);
            yield return new WaitForSeconds(1f);

            if ((_currentStage + 1) % 5 == 0)
                SoundManager.Instance.PlayBGM("Boss");
            _spawner.StartStage(_currentStage);
            state = "StartStage";

            while (!(!_spawner.IsSpawning && _spawner.LeftSlimes == 0))
            {
                state = "spawning : " + Spawner.IsSpawning.ToString() + " / leftSlimes : " + Spawner.LeftSlimes;
                yield return null;
            }

            state = "Stage Clear";
            foreach (SlimeBehaviour slime in _spawner.RecentDead)
            {
                slime.IsLastSlimeToDie = true;
            }
            SoundManager.Instance.PlaySFX("LastSlimeDead");
            yield return Camera.main.GetComponent<CameraController>().LastHitCoroutine();

            yield return new WaitForSeconds(1f);
            GlobalRefs.Player.EverythingStopped = true;
            yield return new WaitForSeconds(_timeAfterStageClear);
            state = "EverythingStopped";

            //??????????
            ResetPlayer();

            if (_currentStage == _spawner.MaxStage - 1)
                break;

            if ((_currentStage + 1) % 5 == 0)
                SoundManager.Instance.PlayBGM("Game");

            yield return GlobalRefs.UpgradeManager.SelectUpgrade(_currentStage);

            state = "upgrade over";
            GlobalRefs.Player.EverythingStopped = false;
            _currentStage++;
        }
        SoundManager.Instance.StopBGM();
        //?????? ??
        yield return new WaitForSeconds(2f);
        OpenGameOver(true);
    }
    
    void ResetPlayer()
    {
        if (GlobalRefs.Player.CurrentState is PlayerStates.InTurretState)
        {
            GlobalRefs.Player.ForceOutOfTurret();
        }
        
        int childs = GlobalRefs.Player.transform.childCount;
        if (childs > 2)
        {
            for (int i = 0; i < childs; i++)
                GlobalRefs.Player.transform.GetChild(i).gameObject.SetActive(true);
            for (int i = 2; i < childs; i++)
                Destroy(GlobalRefs.Player.transform.GetChild(i).gameObject);
        }

        if(GlobalRefs.UpgradeManager.GetCount("?????? ????1") >= 1 || GlobalRefs.UpgradeManager.GetCount("?????? ????2") >= 1)
        {
            Modifier mod1 = new Modifier(_playerAttackSpeed - GlobalRefs.Player.AttackSpeed.Value, Modifier.ApplyType.Add);
            Modifier mod2 = new Modifier(_playerMoveSpeed - GlobalRefs.Player.MoveSpeed.Value, Modifier.ApplyType.Add);
            GlobalRefs.Player.AttackSpeed.AddModifier(mod1);
            GlobalRefs.Player.MoveSpeed.AddModifier(mod2);
        }

    }
    private void Update()
    {
        if(!IsGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            IsPaused = !IsPaused;
            if (IsPaused)
                Pause();
            else
                UnPause();
        }
        if (Input.GetKeyDown(KeyCode.Return) && test)
            _spawner.Test_StopSpawning();
    }
    void Pause()
    {
        IsPaused = true;
        _pausedPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
        SoundManager.Instance.PauseBGM();
    }
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScene");
    }
    public void UnPause()
    {
        StartCoroutine(UnpauseCoroutine());
    }
    IEnumerator UnpauseCoroutine()
    {
        IsPaused = false;
        _pausedPanel.Close();
        while (_pausedPanel.gameObject.activeSelf)
            yield return null;

        SoundManager.Instance.ResumeBGM();
        Time.timeScale = 1;
    }
    public void OnPlayerDead()
    {
        SoundManager.Instance.PlaySFX("GameOver",2);
        StopGame();
        StartCoroutine(PlayerDieCoroutine());
    }
    public void OnFlowerDead()
    {
        SoundManager.Instance.PlaySFX("GameOver",2);
        StopGame();
        StartCoroutine(FlowerDieCoroutine());
    }
    void StopGame()
    {
        IsGameOver = true;
        SoundManager.Instance.StopBGM();

        GlobalRefs.Player.EverythingStopped = true;
        if (_gameLoop != null)
            StopCoroutine(_gameLoop);
        _spawner.StopSpawning();
        _spawner.StopAllSlimes();
    }
    IEnumerator PlayerDieCoroutine()
    {
        yield return new WaitForSecondsRealtime(4f);
        OpenGameOver(false);
    }
    IEnumerator FlowerDieCoroutine()
    {
        yield return new WaitForSecondsRealtime(3f);
        OpenGameOver(false);
    }
    void OpenGameOver(bool win)
    {
        if (win)
            SoundManager.Instance.PlayBGM("GameWin");
        GameOverDataManager.GameOverData data = new GameOverDataManager.GameOverData();
        data.Win = win;
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
            GlobalRefs.UpgradeManager.RerollCount = data._rerollCount;
            SlimeKilled = data._slimesKilled;
        }
    }
}
