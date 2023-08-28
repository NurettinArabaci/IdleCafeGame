using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : Collectable
{
    private SocketController _socketcontroller;
    public SocketController socketController => _socketcontroller ??= GetComponent<SocketController>();


}
