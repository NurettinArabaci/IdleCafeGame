using System.Collections;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    public ProductSo productData{get; private set;}
    [SerializeField] private ProductSo beginProductData,preparedProductData;

    public CollectableType _CollectableType =>  productData.collectableType;
    public ProductType _ProductType =>  productData.productType;

    void Awake()
    {
        productData = beginProductData;
    }



    public void ChangeToPrepared()
    {
        productData = preparedProductData;
        ChangeChildObj();
    }

    private void ChangeChildObj()
    {
        var _childCount= transform.childCount;
        if(_childCount>0)
        {
            for (int i = 0; i < _childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            
        }  
        Instantiate(productData.Prefab,transform.position,Quaternion.identity,transform);
    }

    public void ChangeToBegin()
    {
        productData = beginProductData;
        ChangeChildObj();
    }
}
