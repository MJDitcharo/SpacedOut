using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<Music> music;
    [SerializeField]List<SFX> sfx;
    // Start is called before the first frame update
    

}

public class Music
{
    AudioClip audio;
}
public class SFX
{
    AudioClip audio;
}
