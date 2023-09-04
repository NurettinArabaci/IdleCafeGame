using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoSingleton<SoundManager>
{
    AudioSource source;

    public AudioClip gemCollectSfx,gemSaleSfx,buttonClickSfx;

    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<AudioSource>();

        SoundEvents.OnPlaySfx += PlaySfx;
    }

    private void OnDisable()
    {
        SoundEvents.OnPlaySfx -= PlaySfx;
    }

    void PlaySfx(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

}

