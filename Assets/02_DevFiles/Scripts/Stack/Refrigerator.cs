using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refrigerator : BankArea
{
    [SerializeField] Collectable collectablePrefab;


    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            var obj = Instantiate(collectablePrefab, transform.position, Quaternion.identity);
            socketController.AddStackToInit(obj);
        }
    }
}
