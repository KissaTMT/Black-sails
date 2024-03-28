using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    private const string FADE = "Fade";
    private const string UNFADE = "Unfade";

    public static ScreenFader instance;

    private Animator _animator;

    public void Fade()
    {
        _animator.SetTrigger(FADE);
    }
    public void Unfade()
    {
        _animator.SetTrigger(UNFADE);
    }
    private void Awake()
    {
        SingletonInit();

        _animator = GetComponent<Animator>();
    }
    private void SingletonInit()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
