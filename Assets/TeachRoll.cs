using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachRoll : TutorialBase
{
    // Start is called before the first frame update
    private void Update()
    {
        //check if the player pressed space to roll
        if(Input.GetKeyDown(KeyCode.Space) && prompt.activeInHierarchy)
        {
            StopTeach();
            GameManager.instance.movement.PlayerRoll();
        }
    }
}
