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
    None,
    Mushroom,
    Onion,
    Plate
}

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class ProductSo : ScriptableObject
{
    [SerializeField] private string productName;
    [SerializeField] private Sprite productSprite;
    [SerializeField] private GameObject prefab;
    public bool isLock;
    public ProductType productType;
    public CollectableType collectableType;
    public string ProductName => productName;
    public Sprite ProductSprite => productSprite;
    public GameObject Prefab => prefab;
    


}
