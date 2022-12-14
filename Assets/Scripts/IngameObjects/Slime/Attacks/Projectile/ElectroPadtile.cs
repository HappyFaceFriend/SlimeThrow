using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElectroPadtile : CollisionPadtile
{
    public float _buffProbability;
    public float _duration;
    public void Init(Vector3 targetPosition, SlimeBehaviour shooter)
    {
        base.Init(targetPosition, shooter);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.collider.GetComponent<PlayerBehaviour>();
        var flower = collision.collider.GetComponent<Flower>();
        if (player != null)
        {
            player.OnHittedBySlime(_slime, _damage, transform.position);

            if(GlobalRefs.UpgradeManager.GetCount("비전도체") <= 0)
                player.ApplyBuff(new PlayerBuffs.PlayerShock(_duration));

           if(Random.Range(0f, 1f) <= _buffProbability)
                player.ApplyBuff(new PlayerBuffs.PlayerShock(_duration));


            if (GlobalRefs.UpgradeManager.GetCount("진화") >= 1)
            {
                Modifier modifier = new Modifier(1.1f, Modifier.ApplyType.Multiply);
                GlobalRefs.Player.AttackSpeed.AddModifier(modifier);
            }

            Destroy(gameObject, 0.38f);
        }

    }
    private void Update()
    {
        if (timer < _warningTime)
            timer += Time.deltaTime;
        else
        {
            _canAttack = true;
            Destroy(warning.gameObject);
            child = transform.GetChild(0).gameObject;
            child.SetActive(true);
            collider.enabled = true;
            Destroy(gameObject, 0.3f);
        }
    }
}
