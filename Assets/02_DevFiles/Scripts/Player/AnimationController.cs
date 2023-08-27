using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    [SerializeField] private Animator anim;

    private PlayerController playerController;

    public void SetAnim()
    {

    }

    public AnimationController Init(PlayerController _playerController)
    {
        this.playerController = _playerController;
        return this;
    }

}
