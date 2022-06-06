using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("buttonHover");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySFX("button");
    }

}
