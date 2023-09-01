using System;
using System.Collections;
using UnityEngine;

public class Refrigerator : Stackable
{
    private Animator anim;
    private Animator Anim => anim ??= GetComponentInChildren<Animator>();

    void OnEnable()
    {
        OnGetFromArea+=OnPlayAnimation;
    }

    public void OnPlayAnimation()
    {
        Anim.SetTrigger("OpenCover");
    }

    void OnDisable()
    {
        OnGetFromArea-=OnPlayAnimation;
    }

    
}
