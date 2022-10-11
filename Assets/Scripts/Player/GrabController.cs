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
    TurretBehaviour _turret;
    [SerializeField] float _grabRange;
    [SerializeField] float _pushToTowerRange;
    [SerializeField] Transform _handTransform;
    public SlimeBehaviour GrabbedSlime { get { return _grabbedSlime; } }

    private void Awake()
    {
        _turret = GlobalRefs.Turret;
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
        bool isTurretInRange = Utils.Vectors.IsInDistance(_turret.transform.position, transform.position, _pushToTowerRange);
        if (_turret.IsMouseHovered && isTurretInRange)
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
        Collider2D [] results = Physics2D.OverlapCircleAll(transform.position, _grabRange);

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
