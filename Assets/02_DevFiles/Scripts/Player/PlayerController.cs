using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement _PlayerMovement { get; private set; }
    public PlayerCollider _PlayerCollider { get; private set; }
    public AnimationController _AnimationController { get; private set; }
    public SocketController _SocketController { get; private set; }

    private void CachedComponents()
    {
        _PlayerMovement = GetComponentInChildren<PlayerMovement>().Init(this);
        _AnimationController= GetComponentInChildren<AnimationController>().Init(this);
        _SocketController= GetComponentInChildren<SocketController>().Init(this);
        _PlayerCollider = GetComponentInChildren<PlayerCollider>().Init(this);

    }

    private void Awake()
    {
        CachedComponents();
    }

    


    
}