using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Animator _shovelAnimator;
    [SerializeField] Collider2D _hitBox;
    [SerializeField] Transform _pivot;
    [SerializeField] float _attack;

    PlayerBehaviour _player;
    Animator _hitBoxAnim;
    public bool IsAnimDone
    {
        get
        {
            return _hitBoxAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !_hitBoxAnim.IsInTransition(0);

        }
    }
    private void Awake()
    {
        _player = GetComponent<PlayerBehaviour>();
        _hitBoxAnim = _hitBox.GetComponent<Animator>();
    }

    private void Update()
    {
        if(IsAnimDone)
        {
            Vector3 mouseVec = Utils.Inputs.GetMouseWordPos() - _player.transform.position;
            float rot = Mathf.Atan2(mouseVec.y, mouseVec.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.Euler(new Vector3(0, 0,  rot));

            _pivot.rotation = Quaternion.RotateTowards(_pivot.rotation, newRotation, 1800 * Time.deltaTime);
        }
    }

    public void Attack()
    {
        _shovelAnimator.SetTrigger("onAttack");
        _hitBoxAnim.SetTrigger("Hit");

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
