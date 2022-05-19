using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrompt : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI textMesh;

    private void Start()
    {
        HidePrompt();
    }

    /// <summary>
    /// If nothing is passed in, will show the most recent text passed into ShowPrompt
    /// </summary>
    /// <param name="text"></param>
    public void ShowPrompt(string text)
    {
        textMesh.text = text;
        gameObject.SetActive(true);
    }

    public void HidePrompt()
    {
        gameObject.SetActive(false);
    }
}
