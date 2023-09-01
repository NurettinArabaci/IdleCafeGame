using System;
using System.Collections;
using UnityEngine;

public class CuttingBoard : Stackable
{

    
    [SerializeField] Stackable inputs, outputs;
    
    
    private void Start()
    {
        StartCoroutine(CutCr());
    }

    IEnumerator CutCr()
    {
        yield return new WaitUntil(()=>inputs.GetLastFilledSocket());

        yield return new WaitUntil(()=>GetLastEmptySocket());
        inputs.GetLastFilledSocket().MoveStack(GetLastEmptySocket());
            

        yield return new WaitUntil(()=>GetLastFilledSocket());
        yield return new WaitForSeconds(3);
        
        GetLastFilledSocket().MoveStack(outputs.GetLastEmptySocket());

        StartCoroutine(CutCr());
    }
    



}
