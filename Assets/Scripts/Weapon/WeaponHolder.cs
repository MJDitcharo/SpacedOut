using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int selectedWeapon = 0;
    static public WeaponHolder instance;
    public GameObject gunImages;
    public List<string> unlockedWeapons; //add the pistol by default
    List<KeyCode> keys = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, };
    public int currentChildCount = 0;
    public bool allUnlocked;

    private void Awake()
    {
        instance = this;
        SelectWeapon();
        if (allUnlocked)
            currentChildCount = transform.childCount - 1;
    }

    private void Start()
    {
        gunImages = GameManager.instance.gunImages;
        if (unlockedWeapons.Count == 0)
            unlockedWeapons.Add(gameObject.transform.Find("Pistol").name);
        ReadPlayerPrefs();
    }
    private void Update()
    {
        SwitchWeapons();
        UnequippedWeapons();
        if (allUnlocked)
            currentChildCount = transform.childCount - 1;
    }

    private void SelectWeapon()
    {
        int i = 0;
        //actually equip the weapon
        foreach (Transform t in this.gameObject.transform)
        {
            if (selectedWeapon == i)
            {
                t.gameObject.SetActive(true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            if (i == currentChildCount)
                break;
            i++;
        }

        i = 0;
        //change the gun images
        foreach (Transform t in gunImages.transform)
        {
            if (selectedWeapon == i)
            {
                if (gunImages.transform.Find(t.name) == null)
                    Debug.Log("Not Found: " + t.name);
                gunImages.transform.Find(t.name).gameObject.SetActive(true);
            }
            else
            {
                if (gunImages.transform.Find(t.name) == null)
                    Debug.Log("Not Found: " + t.name);
                gunImages.transform.Find(t.name).gameObject.SetActive(false);
            }
            i++;
        }
    }

    private void SwitchWeapons()
    {
        int prevWeapon = selectedWeapon;

        if (Time.timeScale > 0)
        {
            for (int i = 0; i <= currentChildCount; i++)
            {
                if (allUnlocked)
                    break;
                if (Input.GetKey(keys[i]))
                    selectedWeapon = i;
            }
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

    public float GetWeaponFireRate(WeaponBase.WeaponID weaponID)
    {
        for (int i = 0; i <= currentChildCount; i++)
        {
            if (transform.Find(transform.GetChild(i).name).GetComponent<WeaponBase>().weaponID == weaponID)
                return transform.Find(transform.GetChild(i).name).GetComponent<WeaponBase>().GetFireRateMultiplier();
        }
        return -1f;
    }

    public void UpgradeDamage(string weapon, float multiplier)
    {
        //Debug.Log(transform.Find(weapon).GetComponent<WeaponBase>());
        transform.Find(weapon).GetComponent<WeaponBase>().SetDamageMultiplier(multiplier);
    }

    public float GetWeaponDamage(WeaponBase.WeaponID weaponID)
    {
        for (int i = 0; i <= currentChildCount; i++)
        {
            if (transform.Find(transform.GetChild(i).name).GetComponent<WeaponBase>().weaponID == weaponID)
                return transform.Find(transform.GetChild(i).name).GetComponent<WeaponBase>().GetDamageMultiplier();
        }
        return -1f;
    }

    public bool IsWeaponUnlocked(string weaponName)
    {
        if (unlockedWeapons.Contains(weaponName))
            return true;
        else
            return false;
    }

    public bool AddToUnlockedItems(string weaponName)
    {
        //if (gameObject.transform.Find(weaponName) != null)
        unlockedWeapons.Add(weaponName);
        return true;
    }

    public void ArrangeHierarchy(string weaponName, int index, string tier2Upgrade = "")
    {
        //check if the weapon is unlocked
        if (IsWeaponUnlocked(weaponName))
        {
            //hierarachy for weaponholder
            Transform old = transform.GetChild(index);
            transform.Find(weaponName).SetSiblingIndex(index);
            //hierarchy for gunImages
            Transform oldImage = gunImages.transform.Find(weaponName);
            gunImages.transform.Find(weaponName).SetSiblingIndex(index);
        }
    }

    /// <summary>
    /// puts a in b's place, moves b to bottom
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public void Tier2Upgrade(string tier2Weapon, string baseWeapon, int index)
    {
        int max = transform.childCount - 1;
        int maxImage = gunImages.transform.childCount - 1;
        if (IsWeaponUnlocked(tier2Weapon))
        {
            //hierarachy for weaponholder
            Transform old = transform.GetChild(index);
            int a = transform.GetChild(index).GetSiblingIndex();
            transform.Find(tier2Weapon).SetSiblingIndex(index);
            old.SetSiblingIndex(max);
            //hierarchy for gunImages
            Transform oldImage = gunImages.transform.Find(baseWeapon);
            gunImages.transform.Find(tier2Weapon).SetSiblingIndex(a);
            oldImage.SetSiblingIndex(maxImage);
        }
    }

    private void ReadPlayerPrefs()
    {
        for (int i = 0; i <= currentChildCount; i++)
        {
            WeaponBase id = transform.Find(transform.GetChild(i).name).GetComponent<WeaponBase>();
            switch (id.weaponID)
            {
                case WeaponBase.WeaponID.Pistol:
                    id.SetDamageMultiplier(PlayerPrefs.GetFloat("Pistol Damage"));
                    id.SetFireRateMultiplier(PlayerPrefs.GetFloat("Pistol Fire Rate"));
                    break;
                case WeaponBase.WeaponID.Shotgun:
                    id.SetDamageMultiplier(PlayerPrefs.GetFloat("Shotgun Damage"));
                    id.SetFireRateMultiplier(PlayerPrefs.GetFloat("Shotgun Fire Rate"));
                    break;
                case WeaponBase.WeaponID.Heavy:
                    id.SetDamageMultiplier(PlayerPrefs.GetFloat("Heavy Damage"));
                    id.SetFireRateMultiplier(PlayerPrefs.GetFloat("Heavy Fire Rate"));
                    break;
                case WeaponBase.WeaponID.Rifle:
                    id.SetDamageMultiplier(PlayerPrefs.GetFloat("Rifle  Damage"));
                    id.SetFireRateMultiplier(PlayerPrefs.GetFloat("Rifle Fire Rate"));
                    break;
            }
        }
    }

    public void SaveLoadout()
    {
        //clear the old loadout
        unlockedWeapons.Clear();
        for (int i = 0; i <= currentChildCount; i++)
            unlockedWeapons.Add(transform.GetChild(i).name);
    }

    public string GetEquippedWeaponName(WeaponBase.WeaponID weaponID)
    {
        for (int i = 0; i <= currentChildCount; i++)
        {
            if (weaponID == transform.GetChild(i).GetComponent<WeaponBase>().weaponID)
                return transform.GetChild(i).name;
        }
        return string.Empty;
    }

    public void SwitchWeapons(WeaponBase.WeaponID weaponID)
    {
        //find the weapon with specified id
        WeaponBase weapon = null;
        for (int i = 0; i <= currentChildCount; i++)
        {
            weapon = transform.GetChild(i).GetComponent<WeaponBase>();
            if (weaponID == weapon.weaponID)
            {
                selectedWeapon = i;
                SelectWeapon();
                break;
            }
        }

        if (weapon == null)
        {
            Debug.Log("Weapon Not Found!!");
            return;
        }
        //change the gunimage to the correct weapon
        //change the ammo count to the correct image
        //disable all other weapon images
    }

    public WeaponBase GetEquippedWeapon()
    {
        return transform.GetChild(selectedWeapon).GetComponent<WeaponBase>();
    }
    public string GetWeaponDescription(string weaponName)
    {
        return transform.Find(weaponName).GetComponent<WeaponBase>().weaponDescription;
    }
}
