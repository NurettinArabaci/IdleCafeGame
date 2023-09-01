using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Socket : MonoBehaviour
{
    public Collectable stack;

    public Stackable _myStackable;

    public void Init(Stackable _stackable)
    {
        _myStackable = _stackable;
    }

    public bool isEmpty => !stack;

    public void InitStack(Collectable collectable)
    {
        stack = collectable;
        stack.transform.SetParent(this.transform);
        stack.transform.localPosition = Vector3.zero;

    }
    public void AddStack(Collectable collectable)
    {
        stack = collectable;
        collectable.transform.SetParent(this.transform);
        collectable.transform.DOLocalJump(Vector3.zero, 0.3f, 1, 0.2f);

    }

    public Collectable RemoveStack(Transform parent)
    {
        stack.transform.SetParent(parent);
        stack.transform.DOLocalJump(parent.localPosition, 1, 1, 0.5f);
        
        return stack;
    }

    public void MoveStack(Socket newSocket)
    {
        
        newSocket.AddStack(stack);
        stack = null;
    }


}
