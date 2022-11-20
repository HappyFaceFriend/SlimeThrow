using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStatManager : MonoBehaviour
{
    public BurnInfo _burn;

    private void Awake()
    {
        _burn = new BurnInfo();
    }

}
