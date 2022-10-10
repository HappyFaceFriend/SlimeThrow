using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour, IAttackableBySlime
{
    public void OnHittedBySlime(SlimeBehaviour slime, float damage)
    {
        Debug.Log("Flower took " + damage + " damage");
    }
}
