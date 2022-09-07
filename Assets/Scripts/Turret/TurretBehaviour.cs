using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : StateMachineBase
{
    List<SlimeBehaviour> _loadedSlimes;

    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _shootPosition;

    public List<SlimeBehaviour> LoadedSlimes { get { return _loadedSlimes; } }
    private void Awake()
    {
        _loadedSlimes = new List<SlimeBehaviour>();
    }
    public void PlaceSlime(SlimeBehaviour slime)
    {
        slime.gameObject.SetActive(false);
        slime.transform.SetParent(transform);
        _loadedSlimes.Add(slime);

        Debug.Log(slime.name + " Placed");
    }
    public void Shoot(Vector3 targetPosition)
    {
        GameObject bulletObject = Instantiate(_bulletPrefab);
        bulletObject.transform.position = _shootPosition.position;
        bulletObject.GetComponent<BulletBehaviour>().StartShoot(targetPosition);

        _loadedSlimes.Clear();
    }
    protected override StateBase GetInitialState()
    {

        return new TurretDefaultstate(this);
    }
}
