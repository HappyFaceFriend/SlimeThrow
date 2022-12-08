using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffectBase : MonoBehaviour
{
    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
