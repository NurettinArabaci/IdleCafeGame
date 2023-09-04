using System;
using System.Collections;
using UnityEngine;

public class CuttingBoard : Stackable
{

    
    [SerializeField] private Stackable inputs, outputs;

    private Animator _animator;
    
    private WaitUntil waitInput, waitEmptySocket, waitFillSocket;

    protected override void Awake()
    {
        base.Awake();

         _animator = GetComponentInChildren<Animator>();
        waitInput = new WaitUntil( () => inputs.GetLastFilledSocket());
        waitEmptySocket = new WaitUntil( () => GetLastEmptySocket());
        waitFillSocket= new WaitUntil( () => GetLastFilledSocket());
    }
    private void Start()
    {
        StartCoroutine(CutCr());
    }

    IEnumerator CutCr()
    {
        yield return waitInput;

        yield return waitEmptySocket;
        inputs.GetLastFilledSocket().MoveStack(GetLastEmptySocket());

        yield return waitFillSocket;
        _animator.SetTrigger("Cut");

        yield return new WaitForSeconds(2);
        GetLastFilledSocket().stack.ChangeToData(ProductType.PreparedProduct);
        GetLastFilledSocket().MoveStack(outputs.GetLastEmptySocket());

        StartCoroutine(CutCr());
    }
    



}
