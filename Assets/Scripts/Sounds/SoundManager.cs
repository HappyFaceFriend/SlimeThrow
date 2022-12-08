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
        _bgmSource.Stop();
        _bgmSource.clip = sound;
        _bgmSource.volume = volume;
        _bgmSource.Play();
    }
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PauseBGM()
    {
        _bgmSource.Pause();
    }
    public void ResumeBGM()
    {
        _bgmSource.Play();
    }
    public void PlaySFX(string name, float volume = 0.5f)
    {
        AudioClip sound = _uiSounds.GetSound(name);
        if (sound == null)
            _sfxSource.PlayOneShot(_gameSounds.GetSound(name), volume);
        else

            _sfxSource.PlayOneShot(sound, volume);
    }
    public void PlaySFXDelayed(string name, float delay, float volume = 0.5f)
    {
        StartCoroutine(DelayedSound(name, delay, volume));
    }
    IEnumerator DelayedSound(string name, float delay, float volume)
    {
        yield return new WaitForSecondsRealtime(delay);
        PlaySFX(name, volume);
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
