using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachShoot : TutorialBase
{
    private void Update()
    {
        if(promptCanvas.activeInHierarchy && Input.GetButton("Fire1"))
        {
            StopTeach();
            //WeaponHolder.instance.GetEquippedWeapon().Shoot();
        }
    }
}
