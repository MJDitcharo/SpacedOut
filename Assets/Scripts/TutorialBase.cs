using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBase : PopUpMenu
{
    // Start is called before the first frame update
    [SerializeField]
    protected GameObject prompt;
    protected virtual void Start()
    {
        prompt.SetActive(false);
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
        prompt.SetActive(true);
    }

    protected virtual void StopTeach()
    {
        UnfreezeWorld();
        prompt.SetActive(false); //disable the prompt
        //gameObject.SetActive(false); //disable the object

    }
}
