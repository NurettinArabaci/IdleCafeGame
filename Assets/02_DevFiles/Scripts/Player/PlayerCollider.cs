using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : Cachable<PlayerCollider>
{
    private Stackable _myStackable;
    private void Awake()
    {
        _myStackable = playerController._PlayerStackable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Stackable stackable))
        {
            if(other.TryGetComponent(out SitArea sitArea))
            {   
                if(sitArea.stackType==StackType.Input)
                {
                    var available=_myStackable.GetAvailableFilledSocket(sitArea.collectableType);
                    if(available && !sitArea.GetLastFilledSocket())
                    {
                        available.MoveStack(sitArea.GetLastEmptySocket());
                        return;
                    }
                    return;
                }
                if(sitArea.GetLastFilledSocket())
                {
                    sitArea.GetLastFilledSocket().MoveStack(_myStackable.GetLastEmptySocket());
                    OrderController.Instance.AddListEmptyArea(sitArea);
                }
                    
                return;
            }
            _myStackable.Process(stackable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Stackable stackable))
        {
            /*if(other.TryGetComponent(out SitArea sitArea))
            {   
                if(sitArea.stackType==StackType.Input)
                {
                    _myStackable.StopProcess();
                    return;
                }
                _myStackable.stackType=StackType.None;
                stackable.StopProcess();
                return;
                
            }
            */
            
            _myStackable.StopProcess();
        }

    }
}
