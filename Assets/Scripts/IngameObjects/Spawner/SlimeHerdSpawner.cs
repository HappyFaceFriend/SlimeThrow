using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHerdSpawner : MonoBehaviour
{
    [SerializeField] Transform [] _spawnAreas;
    [SerializeField] float [] _areaWeights = new float[4];
    [SerializeField] Rect  [] _dontSpawnAreas;
    [Header("Settings")]
    [SerializeField] List<StageSpawnData> _spawnDataPerStage;
    List<int> _selectedMainIndicies;
    [Header("Test")]
    [SerializeField] string code = "";

    SlimeHerd _recentHerd = null;


    public StageLabel.Type[] LabelTypes { get; private set; }
    public List<Sprite> LabelImages { get; private set; }
    public bool IsSpawning { get; private set; }
    public int MaxStage { get { return _spawnDataPerStage.Count; } }
    public int LeftSlimes { get { return _spawnedSlimes.Count; } }

    SlimeHerd[] _allHerds;
    List<SlimeHerd>[] _herdsByDifficulty;

    [SerializeField]List<SlimeBehaviour> _spawnedSlimes;
    [SerializeField] float eTime = 0f;

    public bool _isBurningOn { get; set; } = false;
    public bool _stopBurn { get; set; } = true;
    public bool _isSnowyOn { get; set; } = false;
    public bool _stopSnowy { get; set; } = true;
    public BuffableStat ExtraDamage { get; set; }

    List<SlimeBehaviour> _recentDead = new List<SlimeBehaviour>();
    public List<SlimeBehaviour> RecentDead { get { return _recentDead; } }

    Coroutine _specialSpawn = null;
    Coroutine _spawingCoroutine = null;
    private void Awake()
    {
        _allHerds = Resources.LoadAll<SlimeHerd>(Defs.SlimeHerdPrefabsPath);
        _herdsByDifficulty = new List<SlimeHerd>[System.Enum.GetValues(typeof(Difficulty)).Length];
        ExtraDamage = new BuffableStat(1);
        for (int i = 0; i < _herdsByDifficulty.Length; i++)
            _herdsByDifficulty[i] = new List<SlimeHerd>();
        for (int i = 0; i < _allHerds.Length; i++)
            _herdsByDifficulty[(int)_allHerds[i].Difficulty].Add(_allHerds[i]);

        _selectedMainIndicies = new List<int>();
        for (int i = 0; i < MaxStage; i++)
        {
            if (_spawnDataPerStage[i].MainSlimeCandidates.Count == 0)
                _selectedMainIndicies.Add(-1);
            else
                _selectedMainIndicies.Add(Utils.Random.RandomIndex(_spawnDataPerStage[i].MainSlimeCandidates));
        }

        _spawnedSlimes = new List<SlimeBehaviour>();
        //InitLabels();
    }
    public void StartStage(int stage)
    {
        if (_isBurningOn)
            _stopBurn = false;
        if (_isSnowyOn)
            _stopSnowy = false;
        _spawingCoroutine = StartCoroutine(StartSpawning(stage));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach(Rect rect in _dontSpawnAreas)
        {
            Gizmos.DrawWireCube(rect.center, rect.size);
        }
    }
    /*
    void InitLabels()
    {
        LabelTypes = new StageLabel.Type[MaxStage];
        LabelImages = new List<Sprite>();
        for (int i = 0; i < MaxStage; i++)
        {
            LabelTypes[i] = StageLabel.Type.Later;
            LabelImages.Add(_spawnDataPerStage[i].MainSlimeCandidates[_selectedMainIndicies[i]].SlotIcon);
        }
    }*/
    IEnumerator StartSpecialSpawn(StageSpawnData spawnData, int currentStage)
    {
        float specialETime = 0;
        while (IsSpawning)
        {
            specialETime += Time.deltaTime;
            float waitTime = spawnData.SpecialSpawnInterval * Random.Range(0.85f, 1.1f);
            yield return new WaitForSeconds(waitTime);
            if (IsSpawning)
                yield break;

            int currentAreaIdx = Random.Range(0, _spawnAreas.Length);

            if(Random.Range(0f, 1f) < spawnData.SpecialSpawnWeight)
                SpawnHerdRandomPos(Utils.Random.RandomElement(spawnData.Specials), currentAreaIdx);

        }
    }
    IEnumerator StartSpawning(int currentStage)
    {
        int currentAreaIdx = Random.Range(0, _spawnAreas.Length);
        //float eTime = 0f;
        StageSpawnData spawnData = _spawnDataPerStage[currentStage];
        List<SlimeHerd> mustSpawns = new List<SlimeHerd>();
        mustSpawns.AddRange(spawnData.MustSpawns);
        IsSpawning = true;
        eTime = 0;
        if (spawnData.Specials.Count > 0 && spawnData.SpawnInterval > 0)
            _specialSpawn = StartCoroutine(StartSpecialSpawn(spawnData,currentStage));
        while(eTime < spawnData.Duration)
        {
            eTime += Time.deltaTime;
            currentAreaIdx = SelectNextArea(currentAreaIdx);

            SlimeBehaviour mainSlime = GetMainSlime(currentStage, spawnData);
            if(mustSpawns.Count > 0)
            {
                SpawnHerdRandomPos(mustSpawns[0], currentAreaIdx);
                mustSpawns.RemoveAt(0);
            }
            else
                SpawnHerdRandomPos(GetRandomHerd(GetDifficulty(spawnData), mainSlime), currentAreaIdx);

            while (_recentHerd != null)
                yield return null;
            float waitTime = spawnData.SpawnInterval * Random.Range(0.85f, 1.1f);
            if (eTime + waitTime >= spawnData.Duration)
                break;
            float eTime2 = 0f;
            while(eTime2 <= waitTime && eTime < spawnData.Duration)
            {
                eTime2 += Time.deltaTime;
                eTime += Time.deltaTime;
                yield return null;
            }
            if (eTime >= 10)
            {
                _stopBurn = true;
                _stopSnowy = true;
            }
        }
        
        IsSpawning = false;
        if (_specialSpawn != null)
            StopCoroutine(_specialSpawn);
    }
    SlimeBehaviour GetMainSlime(int currentStage, StageSpawnData spawnData)
    {
        SlimeBehaviour mainSlime;
        if (_selectedMainIndicies[currentStage] == -1)
            mainSlime = null;
        else
        {
            mainSlime = spawnData.MainSlimeCandidates[_selectedMainIndicies[currentStage]];

            if (Random.Range(0f, 1f) > spawnData.MainSlimeWeight && spawnData.MainSlimeCandidates.Count > 1)
            {
                int count = 0;
                while (mainSlime != spawnData.MainSlimeCandidates[_selectedMainIndicies[currentStage]] && count < 25)
                {
                    count++;
                    mainSlime = Utils.Random.RandomElement(spawnData.MainSlimeCandidates);
                }
            }
        }
        return mainSlime;

    }
    SlimeHerd GetRandomHerd(Difficulty difficulty, SlimeBehaviour mainSlime)
    {
        List<SlimeHerd> herds = _herdsByDifficulty[(int)difficulty];
        List<SlimeHerd> mainHerds = new List<SlimeHerd>();

        if(mainSlime == null)
        {
            mainHerds.AddRange(herds);
        }
        else
        {
            for (int i = 0; i < herds.Count; i++)
            {
                if (herds[i].MainSlime != null && herds[i].MainSlime.Data == mainSlime.Data)
                    mainHerds.Add(herds[i]);
            }

            if (mainHerds.Count == 0)
            {
                mainHerds.AddRange(herds);
            }
        }

        return Utils.Random.RandomElement(mainHerds);
    }
    Difficulty GetDifficulty(StageSpawnData spawnData)
    {
        float sum = spawnData.EasyWeight + spawnData.NormalWeight + spawnData.HardWeight + spawnData.VeryHardWeight;
        float r = Random.Range(0f, sum);
        float weight = spawnData.EasyWeight;
        if (r < weight)
            return Difficulty.Easy;
        weight += spawnData.NormalWeight;
        if (r < weight)
            return Difficulty.Normal;
        weight += spawnData.HardWeight;
        if (r < weight)
            return Difficulty.Hard;
        return Difficulty.VeryHard;
    }
    Rect GetAreaRect(Transform area)
    {
        return new Rect(area.position - area.localScale / 2, area.localScale);
    }
    int GetNextAreaIdx(int current, int delta)
    {
        return (current + delta + _spawnAreas.Length) % _spawnAreas.Length;
    }
    int SelectNextArea(int current)
    {
        float weightSum = 0;
        for (int i = 0; i < _areaWeights.Length; i++)
            weightSum += _areaWeights[i];
        float r = Random.Range(0f, weightSum);
        int delta = 0;
        for (int i = 0; i < _areaWeights.Length; i++)
        {
            if (r < _areaWeights[i])
            {
                delta = i;
                break;
            }
            else
                r -= _areaWeights[i];
        }
        return GetNextAreaIdx(current, (Random.Range(0, 2) * 2 - 1) * delta);

    }
    public void OnAddNewSlime(SlimeBehaviour slime)
    {
        _spawnedSlimes.Add(slime);
    }
    private void Update()
    {
        var dead = _spawnedSlimes.FindAll(x => !x.IsAlive || x==null);
        _spawnedSlimes.RemoveAll(x => !x.IsAlive || x == null);
        if (dead.Count > 0)
        {
            _recentDead = dead;
            GlobalRefs.LevelManger.SlimeKilled += dead.Count;
        }
    }
    public void StopAllSlimes()
    {
        foreach(SlimeBehaviour slime in _spawnedSlimes)
        {
            if(slime.gameObject.activeInHierarchy)
                slime.ChangeState(new SlimeStates.IdleState(slime));
        }
    }
    public void StopSpawning()
    {
        if (_spawingCoroutine != null)
            StopCoroutine(_spawingCoroutine);
    }
    void SpawnHerdRandomPos(SlimeHerd herd, int areaIdx)
    {
        Vector2 targetPosition = Vector2.zero;
        bool isValid = false;
        Rect areaRect = GetAreaRect(_spawnAreas[areaIdx]);
        int count = 0;
        while (!isValid)
        {
            targetPosition = Utils.Random.RandomVector(
                                areaRect.xMin + herd.HerdSize.x / 2, areaRect.xMax - herd.HerdSize.x / 2,
                                areaRect.yMin + herd.HerdSize.y / 2, areaRect.yMax - herd.HerdSize.y / 2);
            Rect herdRect = new Rect(targetPosition - herd.HerdSize/2, herd.HerdSize);
            count++;
            isValid = true;
            foreach (Rect rect in _dontSpawnAreas)
            {
                if (herdRect.Overlaps(rect))
                {
                    isValid = false;
                    break;
                }
            }
            if (count > 25)
            {
                targetPosition = areaRect.center;
                break;
            }
        }
        _recentHerd = Instantiate(herd, targetPosition, Quaternion.identity);
        _recentHerd.gameObject.SetActive(true);
    }
}
