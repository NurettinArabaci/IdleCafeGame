using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct OrderStruct
{
    public int productCount;
    public ProductSo product;
}
[CreateAssetMenu(fileName = "Order", menuName = "ScriptableObjects/Order")]
public class OrderSo : ScriptableObject
{
    [SerializeField] private OrderStruct[] orderStructs;
    [SerializeField] private string orderName;


    public OrderStruct[] OrderStructs => orderStructs;
    public string OrderName => OrderName;

}
