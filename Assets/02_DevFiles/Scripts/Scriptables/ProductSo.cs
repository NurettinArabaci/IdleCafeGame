using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Product", menuName = "ScriptableObjects/Product")]
public class ProductSo : ScriptableObject
{
    [SerializeField] private string productName;
    [SerializeField] private Sprite productSprite;
    [SerializeField] private bool isLock;


    public string ProductName => productName;
    public Sprite ProductSprite => productSprite;
    public bool IsLock => isLock;


}
