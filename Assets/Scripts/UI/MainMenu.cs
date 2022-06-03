using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    bool savedGame = true;
    [SerializeField] GameObject noDataPopUp = null;
    [SerializeField] GameObject savedDataPopUp = null;
    [SerializeField] private TMP_Text volumeValueText;
    //[SerializeField] private TMP_Text SFXValueText;
    [SerializeField] private Slider volumeSlider;
    //[SerializeField] private Slider SFXSlider;
    [SerializeField] GameObject cPrompt;


    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
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
    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene Index"));
    }

    public void LoadGame()
    {
        if (savedGame == false)
        {

        }
        else
        {
            noDataPopUp.SetActive(true);
            savedDataPopUp.SetActive(false);

        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeValueText.text = volume.ToString("0.0");
    }

    public void ApplyAudioSetting()
    {
        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
        StartCoroutine(Confirm());
    }

    public IEnumerator Confirm()
    {
        cPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        cPrompt.SetActive(false);

    }
    //public void SetSFX(float volume)
    //{
    //    AudioListener.volume = volume;
    //    volumeValueText.text = volume.ToString();
    //}
}
