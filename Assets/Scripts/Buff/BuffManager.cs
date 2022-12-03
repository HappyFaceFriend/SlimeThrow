using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager<T> where T:StateMachineBase
{
    List<Buff<T>> _buffList;

    public BuffManager()
    {
        _buffList = new List<Buff<T>>();
    }
    public void AddBuff(Buff<T> newBuff)
    {
        _buffList.Add(newBuff);
        newBuff.OnStart();
    }

    public bool HasBuff(System.Type buffType)
    {
        var result = _buffList.Find(x => x.GetType() == buffType);
        if (result == null)
            return false;
        return true;
    }
    public void OnUpdate()
    {
        List<Buff<T>> _removeBuffs = new List<Buff<T>>();
        foreach (Buff<T> buff in _buffList)
        {
            buff.OnUpdate();
            if (buff.IsOver())
            {
                buff.OnEnd();
                _removeBuffs.Add(buff);
            }
        }
        foreach (Buff<T> buff in _removeBuffs)
        {
            _buffList.Remove(buff);
        }
    }
    public void TerminateBuff()
    {
        List<Buff<T>> _removeBuffs = new List<Buff<T>>();
        foreach(Buff<T> buff in _buffList)
        {
            buff.OnEnd();
            _removeBuffs.Add(buff);
        }
        foreach(Buff<T> buff in _removeBuffs)
        {
            _buffList.Remove(buff);
        }
    }
}
