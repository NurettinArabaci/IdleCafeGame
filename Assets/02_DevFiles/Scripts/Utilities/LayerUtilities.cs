using UnityEngine;



public static class LayerUtilities
{

    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
public static class LayerKeys
{
    public static readonly LayerMask Plate = LayerMask.GetMask("Plate");
    public static readonly LayerMask Product = LayerMask.GetMask("Product");
    public static readonly LayerMask Radar = LayerMask.GetMask("Radar");




}

