using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] UpgradePanel _upgradePanel;
    List<UpgradeData> _upgrades;

    List<UpgradeData>[] _allUpgradeDatas;
    List<string> _upgradeNames;


    public List<string> UpgradesNames { get { return _upgradeNames; } }

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
    public IEnumerator SelectUpgrade(float [] rarityWeights)
    {
        
        UpgradeData[] datas = Utils.Random.RandomElements(_allUpgradeDatas[0], 3);
        _upgradePanel.SetUpgrades(datas);

        _upgradePanel.Open();
        while(_upgradePanel.IsOpen)
            yield return null;

    }
}
