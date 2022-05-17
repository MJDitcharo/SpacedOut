using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrompt : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI textMesh;

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
