using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SitArea : Stackable
{
   [SerializeField] Money moneyPrefab;
    public bool IsAreaEmpty => !GetLastFilledSocket();

    [SerializeField] private SpriteRenderer _orderImage;

    private OrderStruct _order;
    [SerializeField] private OrderSo orderSo;

    protected override void Awake()
    {
        base.Awake();
        OrderController.Instance.AddListEmptyArea(this);
    }
    void Start()
    {
        orderSo = Resources.Load<OrderSo>("Datas/Order");   
        
        OpenCloseOrderPanel(false);

        stackType= StackType.Input;

    }

    public void OpenCloseOrderPanel(bool state)
    {
        _orderImage.gameObject.SetActive(state);
        if(state == true)
        {
            _orderImage.transform.DOScale(Vector3.one,0.4f);
            return;
        }
        _orderImage.transform.DOScale(Vector3.zero,0.4f);
        
        
    }

    public void ChangeStackType()
    {
        stackType = IsAreaEmpty ? StackType.Input : StackType.Output;
    }
    public void OrderComplete()
    {
        MyStackable.GetLastFilledSocket().stack.ChangeToData(ProductType.CompletedProduct);
    }

    public void CreateMoney(Transform _transform)
    {
        Vector3 randomPose = _transform.forward*1.6f + transform.right * Random.Range(-2f, 2.1f);
        var _money = Instantiate(moneyPrefab,transform.position+Vector3.up*4,Quaternion.identity);
        _money.transform.DOJump(_transform.localPosition - randomPose,3f,1,0.5f).OnComplete(()=> _money._Collider.enabled = true);
    } 


    public void GetRandomOrder()
    {
        var _orderStr = orderSo.OrderStructs;
        
        _order = _orderStr[Random.Range(0, _orderStr.Length)];

        for(int i=0; i < _order.product.Count; i++)
            {
                if(_order.product[i].isLock)
                {
                    GetRandomOrder();
                    return;
                }  
            }
        
        

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
