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
    [SerializeField] List<FlowerPlantPoint> _dirts;

    [SerializeField] public SlimeSpawner _spawner;
    [SerializeField] StagePanel _stagePanel;
    List<string> _upgradeList;

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
        InitFlower();
        LoadGame();
        _stagePanel.Init();
        //Loop
        while (true)
        {
            SaveData data = new SaveData(_spawner.CurrentRound, _spawner.CurrentStage, GlobalRefs.UpgradeManager.UpgradesNames, (double)GlobalRefs.Player.HpSystem.CurrentHp, (double)GlobalRefs.Flower.HPSystem.CurrentHp);
            SaveDataManager.Instance.Save(data);
            _spawner.StartNextStage();
            if (_spawner.IsLastStage)
                break;
            yield return _spawner.WaitUntilStageClear();
            SoundManager.Instance.PlaySFX("LastSlimeDead");
            _stagePanel.SetToNextStage();

            //업그레이드
            yield return GlobalRefs.UpgradeManager.SelectUpgrade();
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
        data.Round = _spawner.CurrentRound;
        data.Stage = _spawner.CurrentStage;

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
        if (data == null)
            _spawner.Init();
        else
        {
            if (data._round == 1 && data._stage == 0)
            {
                _spawner.Init();
            }
            else
            {
                _spawner.Load(data);
                GlobalRefs.UpgradeManager.FindUpgrade(data._upgrades);
                GlobalRefs.Player.HpSystem.ChangeHp(GlobalRefs.Player.MaxHp.Value - (float)data._playerHP);
                GlobalRefs.Flower.HPSystem.ChangeHp(GlobalRefs.Flower.HPSystem.MaxHp.Value - (float)data._flowerHP);
            }
        }
    }
}
