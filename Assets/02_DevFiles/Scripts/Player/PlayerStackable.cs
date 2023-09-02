using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStackable : Stackable
{
    private PlayerController playerController;

    public PlayerStackable Init(PlayerController _playerController)
    {
        this.playerController = _playerController;
        return this as PlayerStackable;
        
    }

    void Start()
    {
        StartCoroutine(CheckSocketEmpty_CR());
    }
    IEnumerator CheckSocketEmpty_CR()
    {
        playerController._AnimationController.SetAnimLayer(1);
        yield return new WaitUntil(()=> MyStackable.GetLastFilledSocket());
        playerController._AnimationController.SetAnimLayer(0);
        yield return new WaitUntil(()=> !MyStackable.GetLastFilledSocket());
        StartCoroutine(CheckSocketEmpty_CR());
    }
}