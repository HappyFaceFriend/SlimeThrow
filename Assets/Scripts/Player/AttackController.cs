using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Collider2D _hitBox;
    [SerializeField] Transform _pivot;
    [SerializeField] float _attack;

    PlayerBehaviour _player;

    public bool IsAnimDone
    {
        get
        {
            if (_hitBox != null)
                return !_hitBox.gameObject.activeSelf;
            else
                return true;
        }
    }
    private void Awake()
    {
        _player = GetComponent<PlayerBehaviour>();
    }

    public void Attack()
    {
        Vector3 mouseVec = Utils.Inputs.GetMouseWordPos() - _player.transform.position;
        _pivot.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(mouseVec.y, mouseVec.x) * Mathf.Rad2Deg));
        _hitBox.gameObject.SetActive(true);

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask(Defs.SlimeLayer));

        List<Collider2D> results = new List<Collider2D>();
        _hitBox.OverlapCollider(filter, results);

        foreach(Collider2D collider in results)
        {
            collider.GetComponent<SlimeBehaviour>().OnHitted(_player, (int)_attack);
        }
    }
}
