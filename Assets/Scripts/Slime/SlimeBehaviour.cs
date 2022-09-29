using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : StateMachineBase
{
    [SerializeField] int _maxHp;
    [SerializeField] Sprite _slotIcon;
    [SerializeField] FlipObjectToPoint _flip;

    int _currentHp;
    SlimeMovement _movement;
    public bool IsGrabbable { get { return CurrentState is SlimeStates.DeadState; } }
    public Sprite SlotIcon { get { return _slotIcon; } }

    public SlimeBulletEffect BulletEffect { get; private set; }
    public FlipObjectToPoint Flipper { get { return _flip; } }


    private void Awake()
    {
        BulletEffect = GetComponent<SlimeBulletEffect>();
        _movement = GetComponent<SlimeMovement>();
        _currentHp = _maxHp;
    }
    new protected void Start()
    {
        base.Start();
    }
    public void SetGrabbed(GrabController grabController)
    {
        ChangeState(new SlimeStates.GrabbedState(this));
    }
    public void OnReleasedAtGround()
    {
        ChangeState(new SlimeStates.DeadState(this));
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
            ChangeState(new SlimeStates.DeadState(this));
    }

    protected override StateBase GetInitialState()
    {
        return new SlimeStates.MoveState(this);
    }
}
