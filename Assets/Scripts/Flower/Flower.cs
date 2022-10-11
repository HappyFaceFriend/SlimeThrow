using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerState
{
    Grab, Planted
}

public class Flower : MonoBehaviour, IAttackableBySlime, IGrababble
{
    int _maxHP = 5;
    FlowerState _flowerstate;
    [SerializeField] Animator _animator;

    int _currentHP;
    public Animator Animator
    {
        get { return _animator; }
    }

    private void Awake()
    {
        _flowerstate = FlowerState.Planted;
        _currentHP = _maxHP;
    }

    public void SetGrabbed(GrabController grabController)
    {
        _flowerstate = FlowerState.Grab;
        Animator.SetTrigger("Grabbed");
    }

    public void OnReleasedAtGround()
    {
        _flowerstate = FlowerState.Planted;
        Animator.SetTrigger("Idle");
    }


    public void OnHittedBySlime(SlimeBehaviour slime, float damage)
    {

    }
}
