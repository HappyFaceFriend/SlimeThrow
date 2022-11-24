using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] string _name;
    [SerializeField] AudioClip _clip;
    public string Name { get { return _name; } }
    public AudioClip Clip { get { return _clip; } }
}
[CreateAssetMenu(menuName = "Sound List")]
public class SoundList : ScriptableObject
{
    [SerializeField] List<Sound> _sounds;
    public AudioClip GetSound(string name)
    {
        Sound sound = _sounds.Find(s => s.Name == name);
        if (sound != null)
            return sound.Clip;
        else
            return null;
    }
}
