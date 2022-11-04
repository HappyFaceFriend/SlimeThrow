using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletStat : MonoBehaviour
{
    [SerializeField] float _bulletMoveSpeed;
    public BuffableStat _moveSpeed;

    private void Awake()
    {
        _moveSpeed = new BuffableStat(_bulletMoveSpeed);
    }
}
