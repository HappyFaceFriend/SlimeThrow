using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrabResult
{
    Success, Fail
}
public class GrabController : MonoBehaviour
{
    SlimeBehaviour _grabbedSlime = null;
    FlowerBehaviour _grabbedFlower = null;
    [SerializeField] TurretBehaviour _turret;
    [SerializeField] Collider2D _grabRange;
    [SerializeField] Collider2D _pushToTowerRange;
    [SerializeField] Transform _handTransform;
    public SlimeBehaviour GrabbedSlime { get { return _grabbedSlime; } }

    public GrabResult GrabFlower()
    {
        _grabbedFlower = GetFlower();
        if (_grabbedFlower == null)
            return GrabResult.Fail;

        _grabbedFlower.transform.SetParent(_handTransform);
        _grabbedFlower.transform.localPosition = Vector3.zero;
        _grabbedFlower.SetGrabbed(this);
        return GrabResult.Success;

    }

    public GrabResult GrabSlime()
    {
        _grabbedSlime = GetClosesetGrabbableSlime();
        if (_grabbedSlime == null)
            return GrabResult.Fail;

        _grabbedSlime.transform.SetParent(_handTransform);
        _grabbedSlime.transform.localPosition = Vector3.zero;
        _grabbedSlime.SetGrabbed(this);
        return GrabResult.Success;
    }
    public void ReleaseSlime()
    {
        var turret = Utils.Collisions.GetCollidedComponent<TurretBehaviour>(_pushToTowerRange);
        if (_turret.IsMouseHovered && turret == _turret)
        {
            _turret.PlaceSlime(_grabbedSlime);
        }
        else
        {
            Vector3 mouseVec = Utils.Inputs.GetMouseWordPos() - transform.position;
            _grabbedSlime.transform.position += mouseVec.normalized;
            _grabbedSlime.transform.SetParent(null);
            _grabbedSlime.OnReleasedAtGround();
        }
        _grabbedSlime = null;

    }
    
    SlimeBehaviour GetClosesetGrabbableSlime()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask(Defs.SlimeLayer));

        List<Collider2D> results = new List<Collider2D>();
        _grabRange.OverlapCollider(filter, results);

        SlimeBehaviour closestSlime = null;
        float distance = -1;
        foreach (Collider2D collider in results)
        {
            SlimeBehaviour slime = collider.GetComponent<SlimeBehaviour>();
            if (slime == null || !slime.IsGrabbable)
                continue;
            if (closestSlime == null)
            {
                distance = Utils.Vectors.GetSquareDistance(transform.position, slime.transform.position);
                closestSlime = slime;
            }
            else
            {
                float newDistance = Utils.Vectors.GetSquareDistance(transform.position, slime.transform.position);
                if (distance > newDistance)
                {
                    distance = newDistance;
                    closestSlime = slime;
                }
            }
        }
        return closestSlime;
    }

    FlowerBehaviour GetFlower()
    {
        return null;
    }
}
