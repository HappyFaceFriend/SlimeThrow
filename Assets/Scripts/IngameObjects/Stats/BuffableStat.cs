using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public enum ApplyType { Add, Multiply }
    public ApplyType Type { get; private set; }
    public float Value { get; private set; }
    public Modifier(float value, ApplyType type)
    {
        Type = type;
        Value = value;
    }
}
public class BuffableStat
{
    public float Value { get; private set; }
    public float BaseValue { get; private set; }

    List<Modifier> _modifiers;

    public BuffableStat(float initialValue)
    {
        BaseValue = initialValue;
        Value = initialValue;
        _modifiers = new List<Modifier>();

    }
    public void ChangeBaseValue(float baseValue)
    {
        BaseValue = baseValue;
        RecalculateValue();
    }
    public void AddModifier(Modifier modifier)
    {
        _modifiers.Add(modifier);
        if (modifier.Type == Modifier.ApplyType.Add)
            Value += modifier.Value;
        else if (modifier.Type == Modifier.ApplyType.Multiply)
            Value *= modifier.Value;
    }
    void RecalculateValue()
    {
        Value = BaseValue;
        for (int i = 0; i < _modifiers.Count; i++)
        {
            if (_modifiers[i].Type == Modifier.ApplyType.Add)
                Value += _modifiers[i].Value;
            else if (_modifiers[i].Type == Modifier.ApplyType.Multiply)
                Value *= _modifiers[i].Value;
        }
    }
    public void RemoveModifier(Modifier modifier)
    {
        int index = _modifiers.FindIndex(x => x.Equals(modifier));
        if (index == -1)
        {
            Debug.LogError("Modifier not removed!!");
            return;
        }
        else
        {
            _modifiers.RemoveAt(index);
            if (modifier.Type == Modifier.ApplyType.Add)
                Value -= modifier.Value;
            else if (modifier.Type == Modifier.ApplyType.Multiply)
            {
                RecalculateValue();
            }
        }
    }
}
