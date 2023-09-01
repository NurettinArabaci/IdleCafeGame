using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement _PlayerMovement { get; private set; }
    public AnimationController _AnimationController { get; private set; }

    private void CachedComponents()
    {
        _PlayerMovement = GetComponentInChildren<PlayerMovement>().Init(this);
        _AnimationController= GetComponentInChildren<AnimationController>().Init(this);

    }

    private void Awake()
    {
        CachedComponents();
    }

    


    
}