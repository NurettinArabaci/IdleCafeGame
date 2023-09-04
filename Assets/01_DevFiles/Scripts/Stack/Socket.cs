using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Socket : MonoBehaviour
{
    public Collectable stack;

    private Transform _stackTransform;

    public Stackable _myStackable;

    public void Init(Stackable _stackable)
    {
        _myStackable = _stackable;
    }

    public bool isEmpty => !stack;

    public void InitStack(Collectable collectable)
    {
        stack = collectable;
        _stackTransform=stack.transform;
        _stackTransform.SetParent(this.transform);
        _stackTransform.localPosition = Vector3.zero;
        

    }
    public void AddStack(Collectable collectable)
    {
        stack = collectable;
        _stackTransform=stack.transform;

        _stackTransform.SetParent(this.transform);
        _stackTransform.DOLocalJump(Vector3.zero, 0.8f, 1, 0.5f);
        _stackTransform.DOLocalRotate(Vector3.zero,0.2f); 

    }

    public Collectable RemoveStack(Transform parent)
    {
        _stackTransform.SetParent(parent);
        _stackTransform.DOLocalJump(parent.localPosition, 1, 1, 0.5f);
        
        return stack;
    }

    public void DeleteStack()
    {
        Destroy(stack.gameObject);
        stack = null;
    }

    public void MoveStack(Socket newSocket)
    {
        
        newSocket.AddStack(stack);
        stack = null;
    }


}
