using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreausePick : MonoBehaviour
{
    [SerializeField] GameObject _coin;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coin = Instantiate(_coin, transform.position + new Vector3(0, 1), Quaternion.identity);
        Destroy(coin, 1f);
        gameObject.SetActive(false);

    }
}
