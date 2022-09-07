using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Collider2D _hitBox;

    PlayerBehaviour _player;

    public bool IsAnimDone
    {
        get
        {
            return true;
        }
    }
    private void Awake()
    {
        _player = GetComponent<PlayerBehaviour>();
    }

    public void Attack()
    {
        _hitBox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        _hitBox.gameObject.SetActive(true);

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask(Defs.SlimeLayer));

        List<Collider2D> results = new List<Collider2D>();
        _hitBox.OverlapCollider(filter, results);

        foreach(Collider2D collider in results)
        {
            collider.GetComponent<SlimeBehaviour>().OnHitted(_player);
        }
    }
}
