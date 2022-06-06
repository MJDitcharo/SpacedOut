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
    [SerializeField]
    private List<string> unlockedWeapons; //add the pistol by default
    List<KeyCode> keys = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, };
    public int currentChildCount = 0;
    const int maxChildCount = 4; //only switch between unlocked weapons, 4 being the maximum

    private void Awake()
    {
        instance = this;
        SelectWeapon();
    }

    private void Start()
    {
        gunImages = GameObject.FindGameObjectWithTag("Gun Images");
        if (unlockedWeapons.Count == 0)
            unlockedWeapons.Add(gameObject.transform.Find("Pistol").name);
    }
    private void Update()
    {
        SwitchWeapons();
        UnequippedWeapons();
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
            if (i == currentChildCount)
                break;
            i++;
        }
    }

    private void SwitchWeapons()
    {
        int prevWeapon = selectedWeapon;

        if (Time.timeScale > 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                Debug.Log("Check Child: " + currentChildCount + "  Selected: " + selectedWeapon);
                if (selectedWeapon == currentChildCount)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                Debug.Log("Check Child: " + currentChildCount + "  Selected: " + selectedWeapon);
                if (selectedWeapon <= 0)
                    selectedWeapon = currentChildCount;
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

    public void UnequippedWeapons()
    {
        int i = currentChildCount;
        foreach (Transform t in transform)
        {
            if (t.GetSiblingIndex() <= i)
                continue;
            else
                t.gameObject.SetActive(false);
            i++;
        }
    }

    public void UpgradeFireRate(string weapon, float multiplier)
    {
        //Debug.Log(transform.Find(weapon).GetComponent<WeaponBase>());
        transform.Find(weapon).GetComponent<WeaponBase>().SetFireRateMultiplier(multiplier);
    }
    public void UpgradeDamage(string weapon, float multiplier)
    {
        //Debug.Log(transform.Find(weapon).GetComponent<WeaponBase>());
        transform.Find(weapon).GetComponent<WeaponBase>().SetDamageMultiplier(multiplier);
    }

    public bool IsWeaponUnlocked(string weaponName)
    {
        if (unlockedWeapons.Contains(weaponName))
            return true;
        else
            return false;
    }

    public bool EquipWeapon(string weaponName)
    {
        bool success;
        //find the weaponName in the gameobject
        //set and return the success bool 
        return true;
    }

    public bool AddToUnlockedItems(string weaponName)
    {
        if (gameObject.transform.Find(weaponName) != null)
        {
            unlockedWeapons.Add(weaponName);
            return true;
        }
        else
            return false;
    }



}
