using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public int selectedWeapon = 0;
    static public WeaponHolder instance;
    [SerializeField] GameObject gunImages;
    List<KeyCode> keys = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, };

    private void Awake()
    {
        instance = this;
        SelectWeapon();
    }

    private void Start()
    {
        gunImages = GameObject.FindGameObjectWithTag("Gun Images");
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
            {
                t.gameObject.SetActive(true);
                gunImages.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                t.gameObject.SetActive(false);
                gunImages.transform.GetChild(i).gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void SwitchWeapons()
    {
        int prevWeapon = selectedWeapon;

        int maxChild = transform.childCount - 1;
        if (Time.timeScale > 0)
        {
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

            for (int i = 0; i < keys.Count; i++)
            {
                if (Input.GetKey(keys[i]))
                    selectedWeapon = i;
            }

            SelectWeapon();
    }
}

    public void UpgradeFireRate(int weapon, float multiplier)
    {
        Debug.Log(transform.GetChild(weapon).GetComponent<WeaponBase>());
        transform.GetChild(weapon).GetComponent<WeaponBase>().SetFireRateMultiplier(multiplier);
    }
    public void UpgradeDamage(int weapon, float multiplier)
    {
        Debug.Log(transform.GetChild(weapon).GetComponent<WeaponBase>());
        transform.GetChild(weapon).GetComponent<WeaponBase>().SetDamageMultiplier(multiplier);
    }
}
