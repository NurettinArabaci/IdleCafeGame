using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    SocketController socketController;

    private void Awake()
    {
        socketController = GetComponentInChildren<SocketController>();
    }

    public void InsertStack(Collectable collectable)
    {
        socketController.AddStack(collectable);
    }
}
