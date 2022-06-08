using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MainMenu mMenu;

    [Header("Sound")]
    [SerializeField] private TMP_Text volumeTextValue;
    [SerializeField] private Slider volumeSlider;
    [HideInInspector] public float musicVolume = 0.1f;

    [SerializeField] private TMP_Text sfxTextValue;
    [SerializeField] private Slider sfxSlider;
    [HideInInspector] public float sfxVolume = 0.4f;
    private static LoadPrefs instance;
    public static LoadPrefs Instance
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
    private void Awake()
    {
        instance = this;
        if (canUse)
        {
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                float volume = PlayerPrefs.GetFloat("MusicVolume",100f);
                volumeTextValue.text = volume.ToString();
                volumeSlider.value = volume;
                musicVolume = volume/100;
            }
            else
            {
                mMenu.ResetAudio("Audio");
            }

            if (PlayerPrefs.HasKey("SFXVolume"))
            {
                float volume = PlayerPrefs.GetFloat("SFXVolume", 100f);
                sfxTextValue.text = volume.ToString();
                sfxSlider.value = volume;
                sfxVolume = volume / 100;
            }
            else
            {
                mMenu.ResetAudio("Audio");
            }
        }
    }
}
