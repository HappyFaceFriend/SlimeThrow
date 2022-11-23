using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonBehaviour<SoundManager>
{

    [SerializeField] AudioSource _bgmSource;
    [SerializeField] AudioSource _sfxSource;


    [SerializeField] SoundList _bgms;
    [SerializeField] SoundList _uiSounds;
    [SerializeField] SoundList _gameSounds;
    public void PlayBGM(string name, float volume = 0.5f)
    {
        AudioClip sound = _bgms.GetSound(name);
            _bgmSource.PlayOneShot(sound, volume);
    }
    public void PlaySFX(string name, float volume = 0.5f)
    {
        AudioClip sound = _uiSounds.GetSound(name);
        if (sound == null)
            _sfxSource.PlayOneShot(_gameSounds.GetSound(name), volume);
        else

            _sfxSource.PlayOneShot(sound, volume);
    }
    void PlayBGM(AudioClip bgm, float volume = 0.5f)
    {
        _bgmSource.PlayOneShot(bgm, volume);
    }
    void PlaySFX(AudioClip sfx, float volume = 0.5f)
    {
        _sfxSource.PlayOneShot(sfx, volume);
    }
}
