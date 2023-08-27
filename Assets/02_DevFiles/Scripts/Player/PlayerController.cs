using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement _PlayerMovement { get; private set; }
    public AnimationController _AnimationController { get; private set; }
    public Radar _Radar { get; private set; }

    [SerializeField] private LayerMask targetLayers;


    private void Start()
    {
        GameObject obj = new GameObject("RadarParent");
        obj.transform.SetParent(transform);
        _Radar = Radar.Spawn(obj, this, 2, targetLayers);
        

    }

}