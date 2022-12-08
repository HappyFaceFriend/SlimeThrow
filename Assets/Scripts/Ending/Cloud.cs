using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _killTime;
    [SerializeField] Transform _directionTarget;


    private void Update()
    {
        transform.position += (_directionTarget.position - transform.position).normalized * _speed * Time.deltaTime;
    }

    private void Start()
    {
        Destroy(gameObject, _killTime);
        transform.localScale *= Random.Range(0.7f, 1.2f);
        transform.position += Utils.Vectors.Vec2ToVec3(Utils.Random.RandomUnitVector2());
    }
}
