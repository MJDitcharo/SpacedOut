using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            return;

        PlayerPrefs.SetInt("Scene Index", SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("Checkpoint Index", 0);
        PlayerPrefs.SetInt("Chest Opened", 0);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene Index"));
    }
}
