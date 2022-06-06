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
    private void Start()
    {
        aSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        instance = this;

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
            aSource.volume = LoadPrefs.Instance.sfxVolume;
        }
        else if(soundEffect.audioType == AudioStyle.music)
        {
            aSource.volume = AudioListener.volume;
        }
        aSource.PlayOneShot(soundEffect.audio);

        
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


