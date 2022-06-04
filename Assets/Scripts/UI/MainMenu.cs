using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    int savedGame;
    [SerializeField] bool audioSaved;
    [SerializeField] GameObject noDataPopUp = null;
    [SerializeField] GameObject savedDataPopUp = null;
    [SerializeField] GameObject overrideSave = null;
    [SerializeField] private TMP_Text volumeValueText;
    [SerializeField] private Slider volumeSlider;

    
    [SerializeField] public TMP_Text sfxVolumeText;
    [SerializeField] public Slider sfxVolueSlider;

    [SerializeField] GameObject cPrompt;
    [SerializeField] float defaultVolume = 0.5f;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        if (PlayerPrefs.GetInt("SavedGame") <= 0)
        {
            overrideSave.SetActive(true);
            PlayerPrefs.SetInt("SavedGame", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SavedGame", 1);
            PlayerPrefs.SetInt("Scene Index", 1);
            PlayerPrefs.SetInt("Checkpoint Index", 0);
            PlayerPrefs.SetInt("Skrap Count", 0);
            PlayerPrefs.SetInt("Player Health", 100);
            PlayerPrefs.SetInt("Max Player Health", 100);
            PlayerPrefs.SetInt("Board Wipes", 0);
            PlayerPrefs.SetInt("Chest Opened", 0);

            PlayerPrefs.SetInt("Pistol Ammo", 45);

            SceneManager.LoadScene(1);
        }
    }
    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene Index"));
    }

    public void LoadGame()
    {
        if (PlayerPrefs.GetInt("SavedGame") > 0)
        {
            Continue();
        }
        else
        {
            noDataPopUp.SetActive(true);
            savedDataPopUp.SetActive(false);

        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume/100;
        PlayerPrefs.SetFloat("MusicVolume", AudioListener.volume * 100);
        volumeValueText.text = volume.ToString("0");
    }
     public void SetSFX(float volume)
    {
        LoadPrefs.Instance.sfxVolume = volume/100;
        PlayerPrefs.SetFloat("SFXVolume", LoadPrefs.Instance.sfxVolume * 100);
        sfxVolumeText.text = volume.ToString("0");
    }

    

    public void ResetAudio(string MenuType)
    {
        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeValueText.text = defaultVolume.ToString("0");
            LoadPrefs.Instance.sfxVolume = defaultVolume;
            sfxVolueSlider.value = defaultVolume;
            sfxVolumeText.text = defaultVolume.ToString("0");
            ApplyAudioSetting(); 
        }
        
    }

    public void ApplyAudioSetting()
    {
        audioSaved = true;
        if (audioSaved == true)
        {
            PlayerPrefs.SetFloat("MusicVolume", AudioListener.volume);
            PlayerPrefs.SetFloat("SFXVolume", LoadPrefs.Instance.sfxVolume);
            StartCoroutine(Confirm());
        }
        audioSaved = false;
    }

    public IEnumerator Confirm()
    {
        cPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        cPrompt.SetActive(false);

    }

}

