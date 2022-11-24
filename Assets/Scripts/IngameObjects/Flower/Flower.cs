using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerState
{
    Grab, Planted
}

public class Flower : MonoBehaviour, IAttackableBySlime, IGrababble
{
    [SerializeField] int _maxHp;
    [SerializeField] HpBar _hpBar;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] Animator _animator;
    FlowerState _flowerstate;

    HpSystem _hpSystem;

    public HpSystem HPSystem { get { return _hpSystem; } }
    public Animator Animator
    {
        get { return _animator; }
    }

    private void Awake()
    {
        _flowerstate = FlowerState.Planted;
        _hpSystem = new HpSystem(_maxHp, OnDie);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
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

    public void OnDie()
    {
        _levelManager.OnFlowerDead();
    }
    public void OnHittedBySlime(SlimeBehaviour slime, float damage)
    {
        Debug.Log("슬라임한테 꽃이 맞음 : " + damage);
        _hpSystem.ChangeHp(-damage);
        EffectManager.InstantiateDamageTextEffect(transform.position, damage, DamageTextEffect.Type.FlowerHitted);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
        SoundManager.Instance.PlaySFX("PlayerHitted");
    }
}
