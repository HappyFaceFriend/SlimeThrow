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
    [SerializeField] float _rangeRadius;
    [SerializeField] float _throwSpeed;
    [SerializeField] Transform _handTransform;
    public SlimeBehaviour GrabbedSlime { get { return _grabbedSlime; } }

    public GrabResult GrabSlime()
    {
        _grabbedSlime = GetClosesetGrabbableSlime();
        if (_grabbedSlime == null)
            return GrabResult.Fail;
        _grabbedSlime.SetGrabbed(this);
        return GrabResult.Success;
    }
    public void ThrowSlime()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _grabbedSlime.SetThrown(Utils.Vectors.Vec2ToVec3(mousePosition), _throwSpeed);
    }
    SlimeBehaviour GetClosesetGrabbableSlime()
    {
        SlimeBehaviour closestSlime = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _rangeRadius);
        float distance = -1;
        foreach (Collider2D collider in colliders)
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
