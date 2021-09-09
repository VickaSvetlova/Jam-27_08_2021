using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InfoLink : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [TextArea]
    [SerializeField] private string textField;
    [SerializeField] private Text text;

    private void Start()
    {
        text.text = textField.ToString();
    }

    public void Play(bool b)
    {
        _animator.SetBool("On", b);
    }
}