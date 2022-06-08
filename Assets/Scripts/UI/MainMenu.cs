using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingBar;

    [SerializeField] public TMP_Text sfxVolumeText;
    [SerializeField] public Slider sfxVolueSlider;

    [SerializeField] GameObject cPrompt;
    [SerializeField] float defaultVolume = 0.2f;
    private AsyncOperation operation;
    public static MainMenu instance;

    private void Start()
    {
        AudioManager.Instance.PlaySFX("MenuMusic");
        //instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StopAllCoroutines();
            operation.allowSceneActivation = true;
        }
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        if (PlayerPrefs.GetInt("SavedGame") <= 0)
        {
            PlayerPrefs.SetInt("SavedGame", 1);
            DefaultPrefs();
        }
        else if(PlayerPrefs.GetInt("SavedGame") >= 0)
        {
            overrideSave.SetActive(true);
        }
    }
    public void DefaultPrefs()
    {
        for(int i = 0; i < 4; i++)
        {
            PlayerPrefs.DeleteKey("Weapon " + i);
        }

        PlayerPrefs.SetInt("SavedGame", 1);
        PlayerPrefs.SetInt("Scene Index", 1);
        PlayerPrefs.SetInt("Checkpoint Index", 0);
        PlayerPrefs.SetInt("Skrap Count", 0);
        PlayerPrefs.SetInt("Player Health", 100);
        PlayerPrefs.SetInt("Max Player Health", 100);
        PlayerPrefs.SetInt("Board Wipes", 0);
        PlayerPrefs.SetInt("Chest Opened", 0);
        PlayerPrefs.SetInt("Child Count", 0);
        PlayerPrefs.SetString("Weapon 0", "Pistol");
        PlayerPrefs.SetInt("Pistol Ammo", 45);
        PlayerPrefs.SetInt("Pistol Page", 0);
        PlayerPrefs.SetInt("Shotgun Page", 0);
        PlayerPrefs.SetInt("Rifle Page", 0);
        PlayerPrefs.SetInt("Heavy Page", 0);

        //store data
        PlayerPrefs.SetInt("PistolPage", 1);
        PlayerPrefs.SetInt("ShotgunPage", 0);
        PlayerPrefs.SetInt("HeavyPage", 0);
        PlayerPrefs.SetInt("RiflePage", 0);

        LoadingScene(1);
    }
    public void Continue()
    {
        LoadingScene(PlayerPrefs.GetInt("Scene Index"));
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
        LoadPrefs.Instance.musicVolume = volume/100;
        PlayerPrefs.SetFloat("MusicVolume", LoadPrefs.Instance.musicVolume * 100);
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
            LoadPrefs.Instance.musicVolume = 10f;
            volumeSlider.value = 10f;
            volumeValueText.text = "10";
            LoadPrefs.Instance.sfxVolume = 100f;
            sfxVolueSlider.value = 1000f;
            sfxVolumeText.text = "10";
            ApplyAudioSetting(); 
        }
        
    }

    public void ApplyAudioSetting()
    {
        audioSaved = true;
        if (audioSaved == true)
        {
            PlayerPrefs.SetFloat("MusicVolume", LoadPrefs.Instance.musicVolume);
            PlayerPrefs.SetFloat("SFXVolume", LoadPrefs.Instance.sfxVolume);
        }
        audioSaved = false;
    }

    void LoadingScene(int levelIndex)
    {
        StartCoroutine(LoadSceneAsync(levelIndex));
    }

    IEnumerator LoadSceneAsync(int levelIndex)
    {
         operation = SceneManager.LoadSceneAsync(levelIndex);
        loadingScreen.SetActive(true);
        operation.allowSceneActivation = false;
        
        yield return new WaitForSeconds(3);
        operation.allowSceneActivation = true;
        //while (!operation.isDone)
        //{
            Debug.Log("Loading");
        //    float loadProgess = Mathf.Clamp01(operation.progress/0.9f);
        //    loadingBar.fillAmount = loadProgess;
        //    yield return null;
        //}
        
            
    }
}

