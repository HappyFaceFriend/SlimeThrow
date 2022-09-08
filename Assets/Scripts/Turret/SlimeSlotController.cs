using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSlotController : MonoBehaviour
{

    public int Count { get { return _count; } }
    [SerializeField] SlimeSlot [] _slots;

    int _count = 0;
    private void Start()
    {
        Clear();
    }
    public void PushSlime(Sprite icon)
    {
        _slots[_count].SetImage(icon);
        _count++;
    }
    public void Clear()
    {
        _count = 0;
        for(int i=0; i<_slots.Length; i++)
        {
            _slots[i].SetImage(null);
        }
    }
}
