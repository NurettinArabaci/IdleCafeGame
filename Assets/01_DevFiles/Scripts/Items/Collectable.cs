using System.Collections;
using UnityEngine;
public class Collectable : MonoBehaviour
{
    public ProductSo productData{get; private set;}
    private Vector3 initScale;
    [SerializeField] private ProductSo beginProductData,preparedProductData,mergedData,completeData;

    public ProductType _ProductType =>  productData.productType;
    public CollectableType _CollectableType =>  productData.collectableType;

    void Awake()
    {
        productData = beginProductData;
        initScale = transform.localScale;
    }

    public void InitScale()
    {
        transform.localScale= initScale;
    }

    public void MergeCollectable(Socket otherSocket)
    {
        
        if(otherSocket.stack._CollectableType == _CollectableType ||
           otherSocket.stack._CollectableType == CollectableType.Merged ||
          _CollectableType==CollectableType.Merged) 
            return;
        
        ChangeToMerge();
        otherSocket.DeleteStack();
        

        






    }

    public void ChangeToData(ProductType type)
    {
        switch (type)
        {
            case ProductType.RawProduct:
                productData = beginProductData;
                break;
            
            case ProductType.PreparedProduct:
                productData = preparedProductData;  
                break;
            
            case ProductType.CompletedProduct:
                productData = completeData;
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
