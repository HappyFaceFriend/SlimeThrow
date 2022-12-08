using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerState
{
    Grab, Planted
}

public class Flower : MonoBehaviour, IAttackableBySlime, IGrababble
{
    [SerializeField] int _maxHp;
    [SerializeField] HpBar _hpBar;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] Animator _animator;
    [SerializeField] Transform[] leaves;
    FlowerState _flowerstate;

    HpSystem _hpSystem;

    public HpSystem HPSystem { get { return _hpSystem; } }
    public HpBar HPBar { get { return _hpBar; } }
    public Animator Animator
    {
        get { return _animator; }
    }

    private void Awake()
    {
        _flowerstate = FlowerState.Planted;
        _hpSystem = new HpSystem(_maxHp, OnDie);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
    }

    public void RecoverHP(int recover)
    {
        _hpSystem.ChangeHp(recover);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            OnHittedBySlime(null, 9999999);
    }
    public void SetGrabbed(GrabController grabController)
    {
        _flowerstate = FlowerState.Grab;
        //Animator.SetTrigger("Grabbed");
    }

    public void OnReleasedAtGround()
    {
        _flowerstate = FlowerState.Planted;
        Animator.SetTrigger("Idle");
    }
    public void OnDie()
    {
        _levelManager.OnFlowerDead();
        DropLeaves();
    }
    void DropLeaves()
    {
        StartCoroutine(DropLeavesCoroutine());
    }
    IEnumerator DropLeavesCoroutine()
    {
        for(int i=0; i<leaves.Length; i++)
        {
            Vector3 impact = Utils.Random.RandomVector(new Vector3(-0.3f, 0f), new Vector3(0.3f, 0.5f)) * 1f;
            StartCoroutine(DropLeafCoroutine(leaves[i], impact));
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator DropLeafCoroutine(Transform leaf, Vector3 impactVec)
    {
        float groundY = transform.position.y + Random.Range(-0.2f, 0.1f);
        float eTime = 0;
        float gravity = -1.5f;
        while (leaf.position.y > groundY)
        {
            eTime += Time.deltaTime;
            impactVec += new Vector3(0, gravity, 0) * Time.deltaTime;
            leaf.position += impactVec * Time.deltaTime;
            yield return null;
        }

    }
    public void OnHittedBySlime(SlimeBehaviour slime, float damage)
    {
        _hpSystem.ChangeHp(-damage);
        EffectManager.InstantiateDamageTextEffect(transform.position, damage, DamageTextEffect.Type.FlowerHitted);
        _hpBar.SetHp((int)_hpSystem.CurrentHp, (int)_hpSystem.MaxHp.Value);
        SoundManager.Instance.PlaySFX("PlayerHitted");
    }
}
