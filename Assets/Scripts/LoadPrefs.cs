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


    private void Awake()
    {
        if (canUse)
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                float volume = PlayerPrefs.GetFloat("MasterVolume",100f);
                volumeTextValue.text = volume.ToString();
                volumeSlider.value = volume;
                AudioListener.volume = volume/100;
            }
            else
            {
                mMenu.ResetAudio("Audio");
            }
        }
    }
}
