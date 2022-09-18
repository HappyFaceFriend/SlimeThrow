using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : StateMachineBase
{
    public bool IsReadyToShoot { get { return _bulletBuilder.Count > 0; } }

    [SerializeField] GameObject _bulletPrefab;
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
        BulletBehaviour bulletObject = Instantiate(_bulletPrefab).GetComponent<BulletBehaviour>();
        bulletObject.transform.position = _shootPosition.position;
        _bulletBuilder.ApplyEffectsToBullet(bulletObject);
        bulletObject.StartShoot(targetPosition);

        _bulletBuilder.Clear();
    }
    protected override StateBase GetInitialState()
    {

        return new TurretDefaultstate(this);
    }
}
