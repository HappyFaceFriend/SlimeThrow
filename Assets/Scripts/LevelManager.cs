using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonBehaviour<LevelManager>
{
    [SerializeField] private Transform _well;
    public Transform Well { get { return _well; } }
}
