using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubbish : Stackable
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
