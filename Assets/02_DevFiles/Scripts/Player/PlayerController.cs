using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement _PlayerMovement { get; private set; }
    public AnimationController _AnimationController { get; private set; }

    public PlayerCollider _PlayerCollider { get; private set; }

    public PlayerStackable _PlayerStackable { get; private set;}

    private void CachedComponents()
    {
        _PlayerMovement = GetComponentInChildren<PlayerMovement>().Init(this);
        _AnimationController= GetComponentInChildren<AnimationController>().Init(this);
        _PlayerCollider = GetComponentInChildren<PlayerCollider>().Init(this);
        _PlayerStackable= GetComponentInChildren<PlayerStackable>().Init(this);


    }

    private void Awake()
    {
        CachedComponents();
    }

    


    
}