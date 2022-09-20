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
    [SerializeField] TurretBehaviour _turret;
    [SerializeField] Collider2D _grabRange;
    [SerializeField] Transform _handTransform;
    public SlimeBehaviour GrabbedSlime { get { return _grabbedSlime; } }

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
        ContactFilter2D filter = new ContactFilter2D();
        List<Collider2D> results = new List<Collider2D>();
        _grabRange.OverlapCollider(filter.NoFilter(), results);

        TurretBehaviour turret = null;
        foreach (Collider2D collider in results)
        {
            turret = collider.GetComponent<TurretBehaviour>();
            if (turret != null)
            {
                turret.PlaceSlime(_grabbedSlime);
                break;
            }
        }
        if (turret == null)
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
}
