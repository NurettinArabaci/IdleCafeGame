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
                    var available=_myStackable.GetAvailableFilledSocket(sitArea.collectableType,sitArea.productType);
                    if(available && !sitArea.GetLastFilledSocket())
                    {
                        sitArea.CreateMoney(transform);
                        available.MoveStack(sitArea.GetLastEmptySocket());
                        
                        return;
                    }
                    return;
                }
                if(sitArea.GetLastFilledSocket())
                {
                    if(!_myStackable.GetLastEmptySocket())return;
                    
                    sitArea.GetLastFilledSocket().MoveStack(_myStackable.GetLastEmptySocket());
                    OrderController.Instance.AddListEmptyArea(sitArea);
                }
                    
                return;
            }
            _myStackable.Process(stackable);
        }

        if(other.TryGetComponent(out Money _money))
        {
            _money.Collect();
            playerController.Money += _money.MoneyAmount;
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
