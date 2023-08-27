using UnityEngine;
using System;

public class CollectableEvents
{
    public static event Action<GameObject> OnCollectableWithStack;
    public static void Fire_OnCollectableWithStack(GameObject go) { OnCollectableWithStack?.Invoke(go); }

    public static event Action OnSaleCollectable;
    public static void Fire_OnSaleCollectable() { OnSaleCollectable?.Invoke(); }

    public static event Action OnMovementLerp;
    public static void Fire_OnMovementLerp() { OnMovementLerp?.Invoke(); }

}