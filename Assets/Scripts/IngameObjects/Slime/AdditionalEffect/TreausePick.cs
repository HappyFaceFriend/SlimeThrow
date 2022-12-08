using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreausePick : MonoBehaviour
{
    [SerializeField] GameObject _coin;
    // Start is called before the first frame update

    bool _isEaten = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isEaten)
            return;
        GameObject coin = Instantiate(_coin, transform.position + new Vector3(0, 1), Quaternion.identity);
        Destroy(coin, 1f);
        GetComponent<Animator>().SetTrigger("Open");
        GlobalRefs.UpgradeManager.RerollCount += 1;
        _isEaten = true;
    }
    public void AnimEvent_Destroy()
    {
        Destroy(gameObject);
    }
}
