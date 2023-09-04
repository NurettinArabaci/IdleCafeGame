using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : Stackable
{
    private Animator anim;
    private Animator Anim => anim ??= GetComponentInChildren<Animator>();

    [SerializeField] private Collectable collectable;

    void OnEnable()
    {
        OnGetFromArea += OnPlayAnimation;

        if (stackType==StackType.Input) return;

        for (int i = 0; i < socketMatris.Length; i++)
        {
            Collectable col = Instantiate(collectable);

            sockets[i].InitStack(col);
            col.InitScale();
            
        }
    }
    public void OnPlayAnimation()
    {
        Anim.SetTrigger("OpenCover");
    }

    void OnDisable()
    {
        OnGetFromArea -= OnPlayAnimation;
    }

}
