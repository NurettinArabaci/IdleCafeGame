using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStackable : Stackable
{
    public PlayerController playerController;

    public PlayerController Init(PlayerController _playerController)
    {
        this.playerController = _playerController;
        return playerController;
        
    }
}
