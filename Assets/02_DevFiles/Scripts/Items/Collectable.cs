using System.Collections;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    public ProductSo productData{get; private set;}
    [SerializeField] private ProductSo beginProductData,preparedProductData,mergedData;

    public ProductType _ProductType =>  productData.productType;
    public CollectableType _CollectableType =>  productData.collectableType;

    void Awake()
    {
        productData = beginProductData;
    }


    public void ChangeToData(ProductType type)
    {
        switch (type)
        {
            case ProductType.RawProduct:
                productData = beginProductData;
                break;
            
            case ProductType.PreaparedProduct:
                productData = preparedProductData;  
                break;
        }
        ChangeChildObj();
    }
    public void ChangeToMerge()
    {
        productData = mergedData;
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

}
