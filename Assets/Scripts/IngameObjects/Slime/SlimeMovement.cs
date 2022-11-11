using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    protected enum State { MoveToTarget, MoveAroundTarget}

    protected State _state;
    public Vector3 TargetPos { get { return _target.position; } }

    protected Transform _player;
    protected Transform _flower;
    protected SlimeBehaviour _slime;

    protected Transform _target;

    protected float _rotateDir;
    protected virtual void Awake()
    {
        _slime = GetComponent<SlimeBehaviour>();
        _state = State.MoveToTarget;
        _player = GlobalRefs.Player.transform;
        _flower = GlobalRefs.Flower.transform;
        _target = GlobalRefs.Flower.transform;
    }
    
    public virtual void OnUpdate()
    {
        if(_state == State.MoveToTarget)
        {
            Vector3 moveDir = (_target.position - transform.position).normalized;
            transform.position += moveDir * _slime.MoveSpeed.Value * Time.deltaTime;

            if (!GlobalRefs.Player.IsTargetable)
                _target = _flower;
            else
            {
                bool isPlayerInRange = Utils.Vectors.IsInDistance(_player.position, transform.position, _slime.SightRange.Value);
                if (_target == _player && !isPlayerInRange)
                    _target = _flower;
                else if (_target == _flower && isPlayerInRange)
                    _target = _player;
            }
            
            if (Utils.Vectors.IsInDistance(_target.position, transform.position, _slime.AttackRange.Value))
            {
                _state = State.MoveAroundTarget;
                Vector3 distanceVec = _target.position - transform.position;
                _rotateDir = Random.Range(0, 2) * 2 - 1; //1 or -1
            }
        }
        else
        {
            Vector3 distanceVec = _target.position - transform.position;
            Vector3 moveDir = new Vector3(-distanceVec.y, distanceVec.x, 0).normalized;
            transform.position += moveDir * _rotateDir * _slime.MoveSpeed.Value * Time.deltaTime;

            if (!Utils.Vectors.IsInDistance(_target.position, transform.position, _slime.AttackRange.Value))
            {
                _state = State.MoveToTarget;
            }
        }

    }
}
