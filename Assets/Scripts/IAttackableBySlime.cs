using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackableBySlime
{
    public void OnHittedBySlime(SlimeBehaviour slime, float damage);
}
