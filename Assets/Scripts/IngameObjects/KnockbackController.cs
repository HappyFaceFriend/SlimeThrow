using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    Coroutine _currentCoroutine;
    public bool SuperArmor { get; set; }
    public bool IsKnockbackDone { get; private set; }

    public Vector3 Velocity { get; private set; }
    public void ApplyKnockback(Vector3 impactPosition, float distance, float speed)
    {
        Vector3 knockbackDir = (transform.position - impactPosition).normalized;

        if (SuperArmor)
        {
            Velocity = knockbackDir;
            return;
        }
        StopKnockback();
        _currentCoroutine = StartCoroutine(KnockbackCoroutine(knockbackDir, distance, speed));
    }
    public void ApplyKnockbackDir(Vector3 knockBackDir, float distance, float speed)
    {
        if (SuperArmor)
        {
            Velocity = knockBackDir;
            return;
        }
        StopKnockback();
        _currentCoroutine = StartCoroutine(KnockbackCoroutine(knockBackDir.normalized, distance, speed));
    }
    public void StopKnockback()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        IsKnockbackDone = true;
        Velocity = Vector3.zero;
    }
    private void Awake()
    {
        IsKnockbackDone = true;
        SuperArmor = false;
        Velocity = Vector3.zero;
    }

    IEnumerator KnockbackCoroutine(Vector3 knockbackDir, float distance, float speed)
    {
        IsKnockbackDone = false;

        float eTime = 0f;
        float duration = distance / speed;

        while(eTime < duration)
        {
            Velocity = Vector3.Lerp(knockbackDir * speed , Vector3.zero, eTime / duration);
            transform.position += Velocity * Time.deltaTime;
            eTime += Time.deltaTime;
            yield return null;
        }
        IsKnockbackDone = true;
        Velocity = Vector3.zero;
    }
}
