using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitArea : Stackable
{
   
    public bool IsAreaEmpty => !GetLastFilledSocket();

    [SerializeField] SpriteRenderer _orderImage;

    public void OpenCloseOrderPanel(bool state)
    {
        _orderImage.gameObject.SetActive(state);
        
    }

    OrderStruct _order;
    [SerializeField] OrderSo orderSo;

    
    void Start()
    {
        orderSo = Resources.Load<OrderSo>("Datas/Order");
        
        OrderController.Instance.AddListEmptyArea(this);
        OpenCloseOrderPanel(false);

        stackType= StackType.Input;

    }

    public void ChangeStackType()
    {
        stackType = IsAreaEmpty ? StackType.Input : StackType.Output;
    }

    public void GetRandomOrder()
    {
        
        var _orderStr = orderSo.OrderStructs;
        _order = _orderStr[Random.Range(0, _orderStr.Length)];

        if(_order.product.Count >= 2)
        {
            collectableType = CollectableType.Merged;
        }
        else
            collectableType = _order.product[0].collectableType ;

        _orderImage.sprite = _order.ProductSprite;
        OpenCloseOrderPanel(true);
        
        ChangeStackType();
    }

    

    

}
