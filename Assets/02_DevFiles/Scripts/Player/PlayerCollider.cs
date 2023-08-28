using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : Cachable<PlayerCollider>
{
   
    private SocketController socketController;
    public SocketController _SocketControl => socketController ??= playerController._SocketController;

    private void OnTriggerEnter(Collider other)
    {


        if (other.TryGetComponent(out Collectable obj))
        {
            if (_SocketControl.cachedLastCollectable == null)
            {
                _SocketControl.AddStack(obj);
                return;
            }

            var lastCollectable = _SocketControl.cachedLastCollectable;

            if (lastCollectable._CollectableType == CollectableType.Plate)
            {
                if (obj._CollectableType != CollectableType.PreaparedProduct) return;
                var platecontroller = lastCollectable as PlateController;
                platecontroller.socketController.AddStack(obj);
            }

            if (lastCollectable._CollectableType == CollectableType.RawProduct)
            {
                if (obj._CollectableType != CollectableType.RawProduct) return;
                _SocketControl.AddStack(obj);
            }
        }

        if (other.TryGetComponent(out Rubish rubish))
        {
            rubish.StockUpArea(_SocketControl.GetFillSockets());
        }

        if (other.TryGetComponent(out Refrigerator refrigerator))
        {
            refrigerator.TakeFromArea(_SocketControl);
        }







    }

}
