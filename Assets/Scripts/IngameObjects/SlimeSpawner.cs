using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeSpawner : MonoBehaviour
{
    [System.Serializable]
    struct SpawnPool
    {
        public int mainSlime;
        //public int bossSlime;
        public List<int> otherSlimes;
    }
    class SpawnSection
    {
        public float from;
        public float to;
        public Utils.Timer spawnTimer;
        public SpawnSection(float from, float to) { this.from = from; this.to = to; }
    }
    [Header("Slime Groups")]
    [SerializeField] SlimeBehaviour _basicSlime;
    [SerializeField] List<SlimeBehaviour> _upgradeSlimes;
    [SerializeField] List<SlimeBehaviour> _attackSlimes;
    [SerializeField] List<SlimeBehaviour> _specialSlimes;
    [SerializeField] List<SlimeBehaviour> _buffSlimes;
    [SerializeField] List<SlimeBehaviour> _bossSlimes;
    List<SlimeBehaviour> _allSlimes = new List<SlimeBehaviour>();
    [Header("Settings")]
    [SerializeField] LevelManager _levelManager;
    [SerializeField] int _spawnSectionCount;
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

    [SerializeField] List<SpawnPool> _spawnPools;
    int maxRound { get { return _spawnSetCodes.Count; } }
    bool _isSpawnDone = true;
    Vector2 _mapSize;
    public StageLabel.Type[] LabelTypes { get; private set; }
    public List<Sprite> LabelImages { get; private set; }
    public bool IsLastStage { get; private set; } = false;

    public bool _burnUpgrade { get; set; } = false;
    public bool _slowUpgrade { get; set; } = false;
    public bool _criticalUpgrade { get; set; } = false;
    public bool _fireBallUpgrade { get; set; } = false;
    public bool _fireSlayerUpgrade { get; set; } = false;
    public bool _flameBulletUpgrade { get; set; } = false;
    public bool _fistUpgrade { get; set; } = false;

    public int CurrentRound { get { return _currentRound; } }
    public int CurrentStage { get { return _currentStage; } }

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
    }
    void InitPools()
    {
        Utils.Random.Shuffle(_attackSlimes);
        _allSlimes.AddRange(_upgradeSlimes);
        _allSlimes.AddRange(_attackSlimes);
        _allSlimes.AddRange(_specialSlimes);
        _allSlimes.AddRange(_buffSlimes);
        _allSlimes.AddRange(_bossSlimes);
        _spawnPools = new List<SpawnPool>();
        List<SlimeBehaviour> mainPool = new List<SlimeBehaviour>();
        int mainPoolIdx = 0;
        mainPool.Add(_attackSlimes[mainPoolIdx++]);
        mainPool.Add(_attackSlimes[mainPoolIdx++]);

        int[] upgrades = new int[_upgradeSlimes.Count];
        for (int j = 0; j < _upgradeSlimes.Count; j++)
            upgrades[j] = _allSlimes.FindIndex(x => x == _upgradeSlimes[j]);

        int[] specials = new int[_specialSlimes.Count];
        for (int j = 0; j < _specialSlimes.Count; j++)
            specials[j] = _allSlimes.FindIndex(x => x == _specialSlimes[j]);
            
        for (int i=2; i<=maxRound-1; i++)
        {
            if (mainPoolIdx < _attackSlimes.Count)
                mainPool.Add(_attackSlimes[mainPoolIdx++]);
            SlimeBehaviour[] attackPool = Utils.Random.RandomElements(mainPool, 3);
            int main = _allSlimes.FindIndex(x => x == attackPool[0]);
            int[] subs = new int[2];
            subs[0] = _allSlimes.FindIndex(x => x == attackPool[1]);
            subs[1] = _allSlimes.FindIndex(x => x == attackPool[2]);

            SpawnPool pool = new SpawnPool();
            pool.mainSlime = main;
            pool.otherSlimes = new List<int>();

            //x - 1 : 메인 + 서브2종 + 강화3종
            pool.otherSlimes.AddRange(subs);
            pool.otherSlimes.AddRange(upgrades);
            _spawnPools.Add(pool);
            pool.otherSlimes.Clear();
            //x - 2 : 메인 + 강화 3종
            pool.otherSlimes.AddRange(upgrades);
            _spawnPools.Add(pool);
            pool.otherSlimes.Clear();
            //x - 3 : 메인 + 서브2종 + 특수전부
            pool.otherSlimes.AddRange(subs);
            pool.otherSlimes.AddRange(specials);
            _spawnPools.Add(pool);
            pool.otherSlimes.Clear();
            //x - 4 : 메인 + 서브2종 + 강화3종
            pool.otherSlimes.AddRange(subs);
            pool.otherSlimes.AddRange(upgrades);
            _spawnPools.Add(pool);
            pool.otherSlimes.Clear();
            //x - 5 : 메인보스 + 메인 + 강화3종
            //보스 넣어야함
            pool.otherSlimes.AddRange(upgrades);
            _spawnPools.Add(pool);
        }
        //label 이미지 설정

        for (int i = 0; i < maxRound-2; i++)
        {
            for (int j = 0; j < _stagePerRound; j++)
            {
                if (j == _stagePerRound - 1)
                {
                    LabelTypes[i * _stagePerRound + j] = StageLabel.Type.Boss;
                    LabelImages.Add(_allSlimes[_spawnPools[i * _stagePerRound + j].mainSlime].SlotIcon);
                }
                else
                {
                    LabelTypes[i * _stagePerRound + j] = StageLabel.Type.Later;
                    LabelImages.Add(_allSlimes[_spawnPools[i * _stagePerRound + j].mainSlime].SlotIcon);
                }
            }
        }
    }
    public void Init()
    {
        _currentRound = 1;
        _currentStage = 0;
        InitPools();
    }
    public void Load(SaveData loadData)
    {
        InitPools();
        _currentRound = loadData._round;
        _currentStage = loadData._stage;
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
            _spawnedSlimes.RemoveAll(x => !x.IsAlive);
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
            if (eTime == 10)
                SetBurn();
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
    public void SetBurn()
    {
        _burnUpgrade = (!_burnUpgrade);
    }
    public void SetSlow()
    {
        _slowUpgrade = (!_slowUpgrade);
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
        _spawnedSlimes.Add(Instantiate(GetRandomSlime(), spawnPoint, Quaternion.identity));
        var slime = _spawnedSlimes[_spawnedSlimes.Count - 1];
        if (_burnUpgrade)
        {
            slime.ApplyBuff(new SlimeBuffs.Burn(4f, 3, 0.8f));
        }
        if (_criticalUpgrade)
            slime.CriticalOn = true;

        if (_fireBallUpgrade)
            slime.FireBallOn = true;

        if (_fireSlayerUpgrade)
            slime.FireSlayerOn = true;

        if (_flameBulletUpgrade)
            slime.FlameBullet = true;

        if (_fistUpgrade)
            slime.BurningFist = true;
    }
    SlimeBehaviour GetRandomSlime()
    {
        SpawnPool currentPool = _spawnPools[CurrentStage - 1 + (CurrentRound - 1) * _stagePerRound];
        float r = Random.Range(0f, 1f);
        if (r < 0.3f)
            return _allSlimes[currentPool.mainSlime];
        else
            return _allSlimes[Utils.Random.RandomElement(currentPool.otherSlimes)];
    }
}