using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : StateMachineBase
{
    public bool IsReadyToShoot { get { return _slimeSlotController.Count > 0; } }

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootPosition;
    [SerializeField] SlimeSlotController _slimeSlotController;

    private void Awake()
    {
    }
    public void PlaceSlime(SlimeBehaviour slime)
    {
        slime.gameObject.SetActive(false);
        slime.transform.SetParent(transform);

        _slimeSlotController.PushSlime(slime.SlotIcon);

        Debug.Log(slime.name + " Placed");
    }
    public void Shoot(Vector3 targetPosition)
    {
        GameObject bulletObject = Instantiate(_bulletPrefab);
        bulletObject.transform.position = _shootPosition.position;
        bulletObject.GetComponent<BulletBehaviour>().StartShoot(targetPosition);

        _slimeSlotController.Clear();
    }
    protected override StateBase GetInitialState()
    {

        return new TurretDefaultstate(this);
    }
}
