using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : StateMachineBase
{ 
    public bool IsReadyToShoot { get { return _bulletBuilder.Count > 0; } }
    public float Height { get { return (_shootPosition.position - transform.position).y; } }
    public Vector3 ShootPosition { get { return _shootPosition.position; } }

    public bool IsMouseHovered { get { return _rigidbody.OverlapPoint(Utils.Inputs.GetMouseWordPos()); } }

    [SerializeField] Transform _shootPosition;
    [SerializeField] BulletBuilder _bulletBuilder;
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void PlaceSlime(SlimeBehaviour slime)
    {
        slime.gameObject.SetActive(false);
        slime.transform.SetParent(transform);

        _bulletBuilder.PushSlime(slime);

        Debug.Log(slime.name + " Placed");
    }
    public void PlacePlayer(PlayerBehaviour player)
    {
        _bulletBuilder.PushPlayer(player);
    }
    public void Shoot(Vector3 targetPosition)
    {
        BulletBehaviour bulletObject = _bulletBuilder.CreateBullet();
        bulletObject.transform.position = _shootPosition.position;
        bulletObject.StartShoot(targetPosition);
    }
    protected override StateBase GetInitialState()
    {
        return new TurretDefaultstate(this);
    }

}