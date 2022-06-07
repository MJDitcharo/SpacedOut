using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private int selectedWeapon = 0;
    static public WeaponHolder instance;
    [SerializeField] GameObject gunImages;
    [SerializeField]
    private List<string> unlockedWeapons; //add the pistol by default
    List<KeyCode> keys = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, };
    public int currentChildCount = 0;
    public bool allUnlocked;
    int maxChildCount = 4; //only switch between unlocked weapons, 4 being the maximum

    private void Awake()
    {
        instance = this;
        SelectWeapon();
        if (allUnlocked)
            maxChildCount = gameObject.transform.childCount - 1;
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
            if(selectedWeapon == i)
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

    public void ArrangeHierarchy(string weaponName, int index, string tier2Upgrade = "")
    {
        //check if the weapon is unlocked
        if(IsWeaponUnlocked(weaponName))
        {
            //hierarachy for weaponholder
            Transform old = transform.GetChild(index);
             transform.Find(weaponName).SetSiblingIndex(index);
            //hierarchy for gunImages
            Transform oldImage = gunImages.transform.Find(weaponName);
            gunImages.transform.Find(weaponName).SetSiblingIndex(index);
            //if (tier2Upgrade != "")
            //{
            //    //swap the guns
            //    SwapToBottom(old, transform.Find(weaponName), transform.childCount);
            //    //swap the gun images
            //    SwapToBottom(oldImage, gunImages.transform.Find(weaponName), gunImages.transform.childCount);
            //}
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
}
