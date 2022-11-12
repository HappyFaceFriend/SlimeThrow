using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Animator _shovelAnimator;
    [SerializeField] Collider2D _hitBox;
    [SerializeField] Transform _pivot;

    PlayerBehaviour _player;
    Animator _hitBoxAnim;


    public bool IsAbleToAttack { get { return _coolDownTimer.IsOver; } }
    Utils.Timer _coolDownTimer;
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
        _coolDownTimer = new Utils.Timer(0f, true);
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
            _coolDownTimer.Tick();
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
            if(collider.GetComponent<SlimeBehaviour>().IsAlive)
                collider.GetComponent<SlimeBehaviour>().OnHittedByPlayer(_player, _player.AttackPower.Value);
            if (!collider.GetComponent<SlimeBehaviour>().IsAlive && _player.UpgradeGetHP)
                _player.HpSystem.ChangeHp(_player._getHP);
        }
        _coolDownTimer.Reset(1f / _player.AttackSpeed.Value);
    }
}
