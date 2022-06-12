using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachShop : TutorialBase
{
    BoxCollider secondTrigger;
    float seconds = 2;
    void Update()
    {
        if (GameManager.instance.shopIsActive)
            StopTeach();
    }
    protected override void Teach()
    {
        promptCanvas.SetActive(true);
        StartCoroutine(LeaveOnScreen());
    }

    private IEnumerator LeaveOnScreen()
    {
        yield return new WaitForSeconds(5);
        StopTeach();
    }
}