using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : Cachable<AnimationController>
{

    [SerializeField] private Animator animator;

    public void SetFloatAnim(string animParam,float value)
    {
        animator.SetFloat(animParam, value);
    }
    public void SetAnimLayer(float weight)
    {
        animator.SetLayerWeight(animator.GetLayerIndex("FullBody"),weight);
    }
}
