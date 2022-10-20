using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff<T>  where T : StateMachineBase
{
    protected T Owner { get; private set; }

    public void SetOwner(T owner)
    {
        Owner = owner;
    }
    public virtual bool IsOver()
    {
        return false;
    }
    public virtual void OnStart()
    {

    }
    public virtual void OnUpdate()
    {
        
    }
    public virtual void OnEnd()
    {

    }
}
