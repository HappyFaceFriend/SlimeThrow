using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackController : MonoBehaviour
{
    Coroutine _currentCoroutine;
    public bool SuperArmor { get; set; }
    public bool IsKnockbackDone { get; private set; }
    public void ApplyKnockback(Vector3 impactPosition, float distance, float speed)
    {
        if (SuperArmor)
            return;
        StopKnockback();
        _currentCoroutine = StartCoroutine(KnockbackCoroutine(impactPosition, distance, speed));
    }
    public void StopKnockback()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
    private void Awake()
    {
        IsKnockbackDone = true;
        SuperArmor = false;
    }

    IEnumerator KnockbackCoroutine(Vector3 impactPosition, float distance, float speed)
    {
        IsKnockbackDone = false;

        Vector3 knockbackDir = (transform.position - impactPosition).normalized;
        float eTime = 0f;
        float duration = distance / speed;

        while(eTime < duration)
        {
            transform.position += Vector3.Lerp( knockbackDir * speed / 2, Vector3.zero, eTime / duration) * Time.deltaTime;
            eTime += Time.deltaTime;
            yield return null;
        }
        IsKnockbackDone = true;
    }
}
