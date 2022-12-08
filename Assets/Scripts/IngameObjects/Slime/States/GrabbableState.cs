using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlimeStates
{
    public class GrabbableState : SlimeState
    {
        bool _isThrown;
        Utils.Timer _timer;
        KnockbackController _knockback;
        Collider2D _collider;
        GameObject _buff;
        public GrabbableState(SlimeBehaviour slime, bool thrown = false) : base("Grabbable", slime)
        {
            _isThrown = thrown;
            _knockback = slime.GetComponent<KnockbackController>();
            _collider = slime.GetComponent<Collider2D>();
            if(slime.transform.childCount == 2)
            {
                _buff = slime.transform.GetChild(1).gameObject;
                _buff.SetActive(false);
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
           
            Slime.Flipper.enabled = false;
            _timer = new Utils.Timer(Slime.GrabbableDuration);
            if (!_isThrown)
                Slime.IsGrabbable = true;
            SetAnimState();

            /*var buff = Slime.transform.GetChild(1).gameObject;
            if(buff != null)
                buff.SetActive(false);*/
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
            if (_isThrown && !GlobalRefs.Turret.IsFull && Utils.Collisions.GetCollidedComponent<TurretBehaviour>(_collider) != null)
            {
                GlobalRefs.Turret.PlaceSlime(Slime);
                return;
            }
            if (_isThrown && _knockback.IsKnockbackDone)
            {
                Slime.IsGrabbable = true;
            }
            _timer.Tick();
            if(_timer.IsOver)
            {
                GameObject.Destroy(Slime.gameObject);
            }
        }
        public override void OnExit()
        {
            base.OnExit();
            Slime.IsGrabbable = false;
        }

    }
}

