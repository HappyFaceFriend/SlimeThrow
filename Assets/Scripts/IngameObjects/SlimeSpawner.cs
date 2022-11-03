using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeSpawner : MonoBehaviour
{
    class SpawnSection
    {
        public float from;
        public float to;
        public Utils.Timer spawnTimer;
        public SpawnSection(float from, float to) { this.from = from; this.to = to; }
    }
    [SerializeField] List<SlimeBehaviour> _slimePrefabs;
    [SerializeField] int _spawnSectionCount;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] int _currentRound;
    [SerializeField] int _currentStage;
    [SerializeField] int _stagePerRound;
    [SerializeField] float _waveDuration;
    [SerializeField] List<string> _spawnSetCodes;
    [Header("Test")]
    [SerializeField] bool _dontSpawn = false;
    List<int[]> _spawnSets;
    List<SpawnSection> _spawnSections;
    [SerializeField] List<SlimeBehaviour> _spawnedSlimes;
    bool _isSpawnDone = true;
    Vector2 _mapSize;
    public StageLabel.Type[] LabelTypes { get; private set; }
    public List<Sprite> LabelImages { get; private set; }
    public bool IsLastStage { get; private set; } = false;
    
    private void Awake()
    {
        _mapSize = _levelManager.MapSize + new Vector2(2,2);
        _spawnSections = new List<SpawnSection>();
        float intervalAngle = 360 / _spawnSectionCount;
        for (int i = 0; i < _spawnSectionCount; i++)
        {
            SpawnSection section = new SpawnSection(i * intervalAngle, (i + 1) * intervalAngle);
            section.spawnTimer = new Utils.Timer(1);
            _spawnSections.Add(section);
        }
        _spawnSets = new List<int[]>();
        foreach (string code in _spawnSetCodes)
        {
            int[] set = new int[code.Length];
            for (int i = 0; i < set.Length; i++)
                set[i] = int.Parse(code.Substring(i, 1));
            _spawnSets.Add(set);
        }
        _spawnedSlimes = new List<SlimeBehaviour>();
        LabelTypes = new StageLabel.Type[_spawnSetCodes.Count * _stagePerRound];
        LabelImages = new List<Sprite>();
        for(int i=0; i<_spawnSetCodes.Count; i++)
        {
            for(int j=0; j<_stagePerRound; j++)
            {
                if (j == _stagePerRound - 1)
                {
                    LabelTypes[i * _stagePerRound + j] = StageLabel.Type.Boss;
                    LabelImages.Add(null);
                }
                else
                {
                    LabelTypes[i * _stagePerRound + j] = StageLabel.Type.Later;
                    LabelImages.Add(null);
                }
            }
        }
    }
    public void Init()
    {
        _currentRound = 1;
        _currentStage = 0;
    }
    void StartStage()
    {
        StartCoroutine(StageCoroutine());
    }
    public void StartNextStage()
    {
        _currentStage += 1;
        if (_currentStage > _stagePerRound)
        {
            _currentStage = 1;
            _currentRound++;
        }
        if(_currentRound > _spawnSetCodes.Count)
        {
            IsLastStage = true;
            return;
        }
        if(!_dontSpawn)
            StartCoroutine(StageCoroutine());
    }
    public IEnumerator WaitUntilStageClear()
    {
        while (! (_isSpawnDone && _spawnedSlimes.Count == 0))
        {
            _spawnedSlimes.RemoveAll(x => x == null);
            yield return null;
        }
    }
    List<SpawnSection> GetRandomSections(int[] sets)
    {
        List<SpawnSection> result = new List<SpawnSection>();
        int totalCount = 0;
        for (int i = 0; i < sets.Length; i++)
            totalCount += sets[i];

        if (totalCount + sets.Length > _spawnSections.Count)
            return null;

        int[] emptySpots = new int[sets.Length];
        for (int i = 0; i < emptySpots.Length; i++)
            emptySpots[i] = 1;
        for (int i = 0; i < _spawnSections.Count - totalCount; i++)
            emptySpots[Random.Range(0, emptySpots.Length)] += 1;

        int index = Random.Range(0, _spawnSections.Count);
        for (int i = 0; i < sets.Length; i++)
        {
            for (int j = 0; j < sets[i]; j++)
            {
                result.Add(_spawnSections[index]);
                index = (index + 1) % _spawnSections.Count;
            }
            index = (index + emptySpots[i]) % _spawnSections.Count;
        }
        return result;
    }
    int GetWaveCount(int round, int stage)
    {
        if (stage == 1 || stage == 2)
            return round;
        if (stage == 3)
        {
            if (round == 1)
                return 1;
            return round + 1;
        }
        if (stage == 4)
        {
            if (round == 1)
                return 2;
            if (round == 2)
                return 3;
            return round + 2;
        }
        if (stage == 5)
            return round * 2;
        return 1;
    }
    IEnumerator StageCoroutine()
    {
        _isSpawnDone = false;
        int waveCount = GetWaveCount(_currentStage, _currentRound);
        List<SpawnSection> sections = GetRandomSections(_spawnSets[_currentRound - 1]);
        for (int i = 0; i < waveCount; i++)
        {
            yield return WaveCoroutine(_waveDuration, sections);
        }
        _isSpawnDone = true;
    }
    IEnumerator WaveCoroutine(float duration, List<SpawnSection> sections)
    {
        foreach (SpawnSection section in sections)
            section.spawnTimer.Reset(GetRandomSpawnInterval(_currentRound));

        float eTime = 0;
        while (eTime < duration)
        {
            eTime += Time.deltaTime;
            foreach (SpawnSection section in sections)
            {
                section.spawnTimer.Tick();
                if (section.spawnTimer.IsOver)
                {
                    SpawnSlime(Random.Range(section.from, section.to));
                    section.spawnTimer.Reset(GetRandomSpawnInterval(_currentRound));
                }
            }
            yield return null;
        }
    }
    float GetRandomSpawnInterval(int round)
    {
        return Random.Range(2.8f - 0.3f * round, 4.4f - 0.4f * round);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            StartStage();
    }
    static float ClampAngle(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
            angle += 360;
        return angle;
    }
    void SpawnSlime(float angle)
    {
        angle = ClampAngle(angle);
        float diagnalAngle = Mathf.Atan2(_mapSize.y / 2, _mapSize.x / 2) * Mathf.Rad2Deg;
        Vector3 spawnPoint = Vector3.zero;
        
        if ((0 <= angle && angle <= diagnalAngle) || (360 - diagnalAngle <= angle && angle <= 360))  //Right
            spawnPoint = new Vector3(_mapSize.x / 2, _mapSize.x / 2 * Mathf.Tan(angle * Mathf.Deg2Rad), 0);
        else if (diagnalAngle <= angle && angle <= 180 - diagnalAngle)  //Up
            spawnPoint = new Vector3(_mapSize.y / 2 / Mathf.Tan(angle * Mathf.Deg2Rad), _mapSize.y / 2);
        else if (180 - diagnalAngle <= angle && angle <= 180 + diagnalAngle)  //Left
            spawnPoint = new Vector3(-_mapSize.x / 2, -_mapSize.x / 2 * Mathf.Tan(angle * Mathf.Deg2Rad), 0);
        else  //Down
            spawnPoint = new Vector3(-_mapSize.y / 2 / Mathf.Tan(angle * Mathf.Deg2Rad), -_mapSize.y / 2);  
        _spawnedSlimes.Add(Instantiate(Utils.Random.RandomElement(_slimePrefabs), spawnPoint, Quaternion.identity));
    }
}