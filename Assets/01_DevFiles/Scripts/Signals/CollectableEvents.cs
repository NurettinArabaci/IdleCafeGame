using UnityEngine;
using System;

public class CollectableEvents
{
    public static event Action OnCollectableLockOpen;
    public static void Fire_OnCollectableLockOpen( ) { OnCollectableLockOpen?.Invoke(); }


}