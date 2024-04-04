using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip bgmSounds;

    public AudioClip[] effectSounds;

    public AudioSource bgm;
    public AudioSource effect;

    protected override void OnAwake()
    {
        bgm.clip = bgmSounds;

        StartCoroutine(Play());
    }

    public void OnVolumeChanged(float volume)
    {
        bgm.volume = volume * 0.5f;

        DataManager.Inst.bgmVolume = volume;
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(0.5f);

        bgm.volume = DataManager.Inst.bgmVolume * 0.5f;

        bgm.Play();
    }
}
