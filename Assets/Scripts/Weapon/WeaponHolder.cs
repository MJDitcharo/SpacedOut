using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public int selectedWeapon = 0;
    static public WeaponHolder instance;


    private void Awake()
    {
        instance = this;
        SelectWeapon();
    }
    private void FixedUpdate()
    {

    }
    private void Update()
    {
        SwitchWeapons();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform t in this.gameObject.transform)
        {
            if (selectedWeapon == i)
                t.gameObject.SetActive(true);
            else
                t.gameObject.SetActive(false);
            i++;
        }
    }

    private bool SwitchWeapons()
    {
        int prevWeapon = selectedWeapon;

        int maxChild = transform.childCount - 1;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= maxChild)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = maxChild;
            else
                selectedWeapon--;
        }

        SelectWeapon();


        if (prevWeapon == selectedWeapon)
            return false;
        else
            return true;
    }
}
