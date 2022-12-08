using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credit : EndingText
{
    [SerializeField] Text _titleText1;
    [SerializeField] Text _titleText2;
    [SerializeField] Text _nameText1;
    [SerializeField] Text _nameText2;

    Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Init(string title, string name)
    {
        _titleText1.text = title;
        _titleText2.text = title;
        _nameText1.text = name;
        _nameText2.text = name;
    }
    public void Kill()
    {
        _animator.SetTrigger("Destroy");
    }
}
