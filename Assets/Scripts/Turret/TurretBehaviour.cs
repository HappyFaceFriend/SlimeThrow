using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : StateMachineBase
{ 
    public bool IsReadyToShoot { get { return _bulletBuilder.Count > 0; } }

    [SerializeField] Transform _shootPosition;
    [SerializeField] BulletBuilder _bulletBuilder;

    private void Awake()
    {
    }
    public void PlaceSlime(SlimeBehaviour slime)
    {
        slime.gameObject.SetActive(false);
        slime.transform.SetParent(transform);

        _bulletBuilder.PushSlime(slime);

        Debug.Log(slime.name + " Placed");
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
