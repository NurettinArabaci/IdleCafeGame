using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private Stackable _myStackable;
    private void Awake()
    {
        _myStackable = GetComponent<Stackable>();
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Stackable stackable))
        {
            _myStackable.Process(stackable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Stackable stackable))
        {
            _myStackable.StopProcess();
        }

    }
}
