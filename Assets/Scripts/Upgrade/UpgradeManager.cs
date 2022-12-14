using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeWeights
{
    public int FromStage;
    public int ToStage;
    public float [] Weights;

}
public class UpgradeManager : MonoBehaviour
{
    [SerializeField] UpgradePanel _upgradePanel;
    [SerializeField] UpgradeWeights[] _upgradeWeights;

    [SerializeField] UpgradeData[] _firstUpgrades;
    [SerializeField] List<UpgradeData> _upgrades;

    List<UpgradeData>[] _allUpgradeDatas;
    List<string> _upgradeNames;

    public int RerollCount { get { return _rerollCount; } set { _rerollCount = value; } }

    [SerializeField] public int _rerollCount= 1;
    
    public List<string> UpgradesNames { get { return _upgradeNames; } }

    public List<UpgradeData> SelectedUpgrades { get { return _upgrades; } }

    private void Awake()
    {
        _upgrades = new List<UpgradeData>();
        _upgradeNames = new List<string>();
        UpgradeData [] allDatas = Resources.LoadAll<UpgradeData>(Defs.UpgradeAssetsPath);
        _allUpgradeDatas = new List<UpgradeData>[System.Enum.GetValues(typeof(Rarity)).Length];
        for (int i = 0; i < _allUpgradeDatas.Length; i++)
            _allUpgradeDatas[i] = new List<UpgradeData>();
        for (int i = 0; i < allDatas.Length; i++)
            _allUpgradeDatas[(int)allDatas[i].Rarity].Add(allDatas[i]);

    }
    public void OnRerollClicked()
    {
        if(RerollCount > 0)
        {
            RerollCount -= 1;
            UpgradeData[] datas = GetRandomUpgrades();
            _upgradePanel.SetUpgrades(datas);
        }
    }

    UpgradeData[] GetRandomUpgrades()
    {
        UpgradeWeights upgradeWeights = _upgradeWeights[0];
        for (int i = 0; i < _upgradeWeights.Length; i++)
        {
            if (_upgradeWeights[i].FromStage <= GlobalRefs.LevelManger.CurrentStage && GlobalRefs.LevelManger.CurrentStage < _upgradeWeights[i].ToStage)
            {
                upgradeWeights = _upgradeWeights[i];
                break;
            }
        }
        UpgradeData[] datas = GetRandomUpgrades(upgradeWeights);
        return datas;
    }
    public void AddUpgrade(UpgradeData data)
    {
        _upgrades.Add(data);
        _upgradeNames.Add(data.Name);
        data.OnAdded();
    }

    private void Update()
    {
        foreach (var upgrade in _upgrades)
        {
            upgrade.OnUpdate();
        }
    }

    public void FindUpgrade(List<string> loadUpgrades)
    {
        foreach(List<UpgradeData> datas in _allUpgradeDatas)
        {
            foreach (UpgradeData data in datas)
            {
                foreach (string dataName in loadUpgrades)
                {
                    if (data.Name == dataName)
                    {
                        AddUpgrade(data);
                    }
                }
            }
        }
    }

    public float GetGrabProbability(SlimeData slime)
    {
        float result = 0f;
        foreach(var upgrade in _upgrades)
        {
            if( upgrade is Upgrades.SlimePickup )
            {
                if ((upgrade as Upgrades.SlimePickup).Slime == slime)
                    result += (upgrade as Upgrades.SlimePickup).Probability;
            }
        }
        return result;
    }
    public int GetCount(string name)
    {
        return _upgrades.FindAll(x => x.Name == name).Count;
    }
    public int GetCount(UpgradeData data)
    {
        return _upgrades.FindAll(x => x == data).Count;
    }
    public UpgradeData[] GetRandomUpgrades(UpgradeWeights weights)
    {
        UpgradeData[] datas = new UpgradeData[weights.Weights.Length];
        for (int i = 0; i < datas.Length; i++)
        {
            int rarity = Utils.Random.RandomIndexByWeight(weights.Weights);

            bool needReroll = true;
            int count = 0;
            while (_allUpgradeDatas[rarity].Count == _upgrades.FindAll(x => (int)(x.Rarity) == rarity).Count)

            {
                rarity -= 1;
                if (rarity == 0)
                    break;
            }
            while (needReroll && count < 50)
            {
                datas[i] = Utils.Random.RandomElement(_allUpgradeDatas[rarity]);
                needReroll = false;
                for (int j = 0; j < i; j++)
                {
                    if (datas[j] == datas[i])
                    {
                        needReroll = true;
                        break;
                    }
                }
                if (datas[i].MaxCount > 0 && _upgrades.FindAll(x => x.Name == datas[i].Name).Count >= datas[i].MaxCount)
                    needReroll = true;
                count++;
            }
        }
        return datas;
    }
    public IEnumerator SelectUpgrade(int currentStage)
    {
        if(currentStage == 0)
        {
            _upgradePanel.SetUpgrades(_firstUpgrades);
        }
        else
        {
            UpgradeData[] datas = GetRandomUpgrades();
            _upgradePanel.SetUpgrades(datas);
        }

        _upgradePanel.Open();
        while(_upgradePanel.IsOpen)
            yield return null;

    }
}
