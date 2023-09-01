using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    None,
    RawProduct,
    PreaparedProduct,
    Plate
}

public enum ProductType
{
    Mushroom,
    Onion
}

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class ProductSo : ScriptableObject
{
    [SerializeField] private string productName;
    [SerializeField] private Sprite productSprite;
    [SerializeField] private bool isLock;

    public ProductType productType;
    public CollectableType collectableType;
    public string ProductName => productName;
    public Sprite ProductSprite => productSprite;
    public bool IsLock => isLock;


}
