using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GrabResult
{
    Success, Fail
}
public enum GrabbingObject
{
    Slime, Flower
}
public class GrabController : MonoBehaviour
{
    SlimeBehaviour _grabbedSlime = null;

    Flower _grabbedFlower = null;
    TurretBehaviour _turret;
    [SerializeField] float _grabRange;
    [SerializeField] float _pushToTowerRange;
    [SerializeField] Transform _handTransform;
    GrabbingObject _grabbingobject;
    public SlimeBehaviour GrabbedSlime { get { return _grabbedSlime; } }
    public Flower GrabbedFlower { get { return _grabbedFlower; } }

    PlayerBehaviour _player;
    private void Awake()
    {
        _turret = GlobalRefs.Turret;
        _player = GetComponent<PlayerBehaviour>();
    }

    public GrabResult GrabFlower()
    {
        _grabbedFlower = GetClosestFlower();
        if (_grabbedFlower == null)
            return GrabResult.Fail;

        _grabbedFlower.transform.SetParent(_handTransform);
        _grabbedFlower.transform.localPosition = Vector3.zero;
        _grabbingobject = GrabbingObject.Flower;
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
        _grabbingobject = GrabbingObject.Slime;
        return GrabResult.Success;
    }

    public void Release()
    {
        if (_grabbingobject == GrabbingObject.Flower)
            ReleaseFlower();
        else if (_grabbingobject == GrabbingObject.Slime)
            ReleaseSlime();

    }
    public void ReleaseSlime()
    {
        Vector3 mouseVec = Utils.Inputs.GetMouseWordPos() - transform.position;
        _grabbedSlime.transform.SetParent(null);
        _grabbedSlime.OnReleasedAtGround(mouseVec.normalized, _player.PushToTowerRange.Value);
        _grabbedSlime = null;
        SoundManager.Instance.PlaySFX("ThrowAway");
    }
    public void ReleaseFlower()
    {
        FlowerPlantPoint _dirt = FindClosestDirt();
        if (_dirt != null)
        {
            _grabbedFlower.transform.position = _dirt.transform.position;
            _grabbedFlower.transform.SetParent(_dirt.transform);
            _grabbedFlower.OnReleasedAtGround();
            _grabbedFlower = null;
        }
        else if(GlobalRefs.UpgradeManager.GetCount("Gardener") > 0)
        {
            _grabbedFlower.transform.position = transform.position;
            _grabbedFlower.transform.SetParent(null);
            _grabbedFlower.OnReleasedAtGround();
            _grabbedFlower = null;
        }
        SoundManager.Instance.PlaySFX("ThrowAway");
    }

    FlowerPlantPoint FindClosestDirt()
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, _grabRange);

        FlowerPlantPoint closestDirt = null;
        float distance = -1;
        foreach (Collider2D collider in results)
        {
            FlowerPlantPoint dirt  = collider.GetComponent<FlowerPlantPoint>();
            if ( dirt == null)
                continue;
            if (closestDirt == null)
            {
                distance = Utils.Vectors.GetSquareDistance(transform.position, dirt.transform.position);
                closestDirt = dirt;
            }
            else
            {
                float newDistance = Utils.Vectors.GetSquareDistance(transform.position, dirt.transform.position);
                if (distance > newDistance)
                {
                    distance = newDistance;
                    closestDirt = dirt;
                }
            }
        }
        return closestDirt;
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

    Flower GetClosestFlower()
    {
        Collider2D[] results = Physics2D.OverlapCircleAll(transform.position, _grabRange);

        Flower closestFlower = null;
        float distance = -1;
        foreach(Collider2D collider in results)
        {
            Flower flower = collider.GetComponent<Flower>();
            if (flower == null)
                continue;
            if(closestFlower == null)
            {
                distance = Utils.Vectors.GetSquareDistance(transform.position, flower.transform.position);
                closestFlower = flower;
            }
            else
            {
                float newDistance = Utils.Vectors.GetSquareDistance(transform.position, flower.transform.position);
                if(distance > newDistance)
                {
                    distance = newDistance;
                    closestFlower = flower;
                }
            }
        }
        return closestFlower;
    }
}
