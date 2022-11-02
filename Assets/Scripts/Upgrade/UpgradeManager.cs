using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] UpgradePanel _upgradePanel;
    List<UpgradeData> _upgrades;

    UpgradeData[] _allUpgradeDatas;

    private void Awake()
    {
        _upgrades = new List<UpgradeData>();
        _allUpgradeDatas = Resources.LoadAll<UpgradeData>(Defs.UpgradeAssetsPath);
    }
    public void AddUpgrade(UpgradeData data)
    {
        _upgrades.Add(data);
        data.OnAdded();
    }

    private void Update()
    {
        foreach (var upgrade in _upgrades)
        {
            upgrade.OnUpdate();
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
    public IEnumerator SelectUpgrade()
    {
        UpgradeData[] datas = Utils.Random.RandomElements(_allUpgradeDatas, 3);
        _upgradePanel.SetUpgrades(datas);

        _upgradePanel.Open();
        while(_upgradePanel.IsOpen)
            yield return null;

    }
}
