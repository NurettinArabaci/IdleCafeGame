using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType
{
    None,
    RawProduct,
    PreparedProduct,
    CompletedProduct,
    Plate,
    All
}

public enum CollectableType
{
    None,
    Mushroom,
    Onion,
    Merged,
    Plate
    
}

[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class ProductSo : ScriptableObject
{
    [SerializeField] private string productName;
    
    [SerializeField] private GameObject prefab;
    public bool isLock;
    public CollectableType collectableType;
    public ProductType productType;
    public string ProductName => productName;
    public GameObject Prefab => prefab;
    


}
