using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBase : PopUpMenu
{
    // Start is called before the first frame update
    [SerializeField]
    protected GameObject promptCanvas;
    protected virtual void Start()
    {
        promptCanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.player)
        {
            Teach();
        }
    }

    protected virtual void Teach()
    {
        FreezeWorld();
        promptCanvas.SetActive(true);
    }

    protected virtual void StopTeach()
    {
        UnfreezeWorld();
        promptCanvas.SetActive(false); //disable the prompt
        gameObject.SetActive(false); //disable the object

    }
}
