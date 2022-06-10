using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    public List<SFX> sfx = new List<SFX>();
    // Start is called before the first frame update
    public Dictionary<string, Sound> soundStorage = new Dictionary<string, Sound>();
    [SerializeField] AudioSource aSource;
    [SerializeField] AudioSource musicSource;
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    private void Update()
    {
        aSource.volume = LoadPrefs.Instance.sfxVolume;
        musicSource.volume = LoadPrefs.Instance.musicVolume;

    }
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < sfx.Count; i++)
        {
            soundStorage.Add(sfx[i].audioTyping,sfx[i].sound);
        }
    }

    public void PlaySFX(string audioType)
    {
        Sound soundEffect = soundStorage[audioType];

        if (soundEffect.audioType == AudioStyle.sfx)
        {
            aSource.PlayOneShot(soundEffect.audio);
        }
        else if(soundEffect.audioType == AudioStyle.music)
        {
            musicSource.clip = soundEffect.audio;
            musicSource.Play();
        }
    }
    public void PlaySFX(Sound soundEffect)
    {
        if (soundEffect.audioType == AudioStyle.sfx)
        {
            aSource.PlayOneShot(soundEffect.audio);
        }
        else if (soundEffect.audioType == AudioStyle.music)
        {
            musicSource.clip = soundEffect.audio;
            musicSource.Play();
        }
    }
}

[System.Serializable]
public class SFX
{
    public Sound sound;
    public string audioTyping;
}
[System.Serializable]
public class Sound
{
    public AudioStyle audioType;
    public AudioClip audio;
}

public enum AudioStyle
{
    sfx,
    music
}


