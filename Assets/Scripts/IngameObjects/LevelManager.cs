using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Vector2 MapSize { get { return _mapSize; } }
    [SerializeField] Vector2 _mapSize;
    [SerializeField] string _gameOverSceneName;
    [SerializeField] Flower _flower;
    [SerializeField] List<FlowerPlantPoint> _dirts;

    [SerializeField] public SlimeSpawner _spawner;
    [SerializeField] StagePanel _stagePanel;
    Utils.Timer BurnTimer;

    private void Start()
    {
        if (_spawner != null)
            StartCoroutine(GameLoop());
    }

    public void setBurn()
    {
        _spawner.SetBurn();
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
        _spawner.Init();
        _stagePanel.Init();
        //Loop
        while (true)
        {
            _spawner.StartNextStage();
            if (_spawner.IsLastStage)
                break;
            yield return _spawner.WaitUntilStageClear();
            _stagePanel.SetToNextStage();

            //업그레이드
            yield return GlobalRefs.UpgradeManager.SelectUpgrade();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
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
}
