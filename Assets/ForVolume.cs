using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForVolume : MonoBehaviour
{
    [SerializeField] private TMP_Text volumeValueText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] public TMP_Text sfxVolumeText;
    [SerializeField] public Slider sfxVolueSlider;
    // Start is called before the first frame update
    public void SetVolume(float volume)
    {
        LoadPrefs.Instance.musicVolume = volume / 100;
        PlayerPrefs.SetFloat("MusicVolume", LoadPrefs.Instance.musicVolume * 100);
        volumeValueText.text = volume.ToString("0");
    }
    public void SetSFX(float volume)
    {
        LoadPrefs.Instance.sfxVolume = volume / 100;
        PlayerPrefs.SetFloat("SFXVolume", LoadPrefs.Instance.sfxVolume * 100);
        sfxVolumeText.text = volume.ToString("0");
    }
}
