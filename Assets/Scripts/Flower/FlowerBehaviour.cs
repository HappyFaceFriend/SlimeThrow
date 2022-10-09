using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBehaviour : StateMachineBase, IGrababble
{
    int _maxHP = 5;
    [SerializeField] Sprite _sloticon;

    int _currentHP;
    public Sprite SlotIcon { get { return _sloticon; } }

    private void Awake()
    {
        _maxHP = 5;
    }

    public void SetGrabbed(GrabController grabController)
    {
        ChangeState(new FlowerStates.GrabbedState(this));
    }

    public void OnReleasedAtGround()
    {
        ChangeState(new FlowerStates.PlantedState(this));
    }

    public void TakeDamage()
    {
        _currentHP -= 1;
    }
}
