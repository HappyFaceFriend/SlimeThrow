using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{
    [SerializeField] int _maxHp;
    [SerializeField] Sprite _slotIcon;

    int _currentHp;
    public bool IsGrabbable { get { return CurrentState is SlimeDeadState; } }
    public Sprite SlotIcon { get { return _slotIcon; } }

    public SlimeBulletEffect BulletEffect { get; private set; }
    private void Awake()
    {
        BulletEffect = GetComponent<SlimeBulletEffect>();
        _currentHp = _maxHp;
    }
    new protected void Start()
    {
        base.Start();
    }

    public void SetGrabbed(GrabController grabController)
    {
        ChangeState(new GrabbedState(this));
    }
    public void OnReleasedAtGround()
    {
        ChangeState(new SlimeDeadState(this));
    }
    public void OnHitted(PlayerBehaviour player, int damage)
    {
        TakeDamage(damage);
    }
    public void OnHittedByBullet(int damage)
    {
        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        if(_currentHp <= 0)
            ChangeState(new SlimeDeadState(this));
    }

    protected override StateBase GetInitialState()
    {
        return new SlimeMoveState(this);
    }
}
