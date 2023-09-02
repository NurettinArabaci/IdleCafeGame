using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SitArea : Stackable
{
    private bool isAreaEmpty;
    public bool IsAreaEmpty => isAreaEmpty;

    public ProductSo GetRandomOrder(OrderSo order)
    {
        return order.OrderStructs[0].product[Random.Range(0, order.OrderStructs[0].product.Count)];
    }

    void Start()
    {
        OrderController.Instance.AddListEmptyArea(this);
    }

}
