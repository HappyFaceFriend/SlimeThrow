using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSlime : MonoBehaviour
{
    [SerializeField] SlimeBehaviour[] _candidates;

    public SlimeBehaviour InstantiateRandom()
    {
        return Instantiate(Utils.Random.RandomElement(_candidates), transform.position, Quaternion.identity);
    }
}
