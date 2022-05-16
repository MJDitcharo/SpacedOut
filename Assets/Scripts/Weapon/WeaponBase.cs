using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    protected enum WeaponID { Pistol, Shotgun, Heavy, Rifle, Melee };
    [SerializeField]
    private static bool weaponOnStart = false;
    protected WeaponID weaponID;
    public Transform firePoint;
    [SerializeField]
    public GameObject bulletPrefab; //instance of bullet prefab

    [SerializeField]
    protected float fireRate = 7f; //firerate
    [SerializeField]
    public float bulletForce = 20f;
    [SerializeField]
    public int ammoCount; //ammo count 
    [SerializeField]
    private int maxAmmo;
    protected float damage; //damage
    protected float nextShotFired = 0f; //counter for next bullet that is fired
                                        // Update is called once per frame

    private void Start()
    {
        //grab ammo for UI. Only do this one time
        if (!weaponOnStart)
        {
            UpdateVisual();
            weaponOnStart = true;
        }
    }
    private void OnEnable()
    {
        if (weaponOnStart)
        {
            UpdateVisual();
        }
    }

    public virtual void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShotFired && ammoCount != 0) //if the first mouse button is down
        {
            nextShotFired = Time.time + 1f / fireRate; //delay for the next bullet fired
            Shoot(); //shoot method
        }
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); //add a force in the up vector

        //deplete ammo
        ammoCount--;
        GameManager.instance.ammoCount.SubtractAmmo();
    }

    public virtual void SetActive(bool isActive) //set current weapon to sctive
    {
        gameObject.SetActive(isActive);
    }

    private void UpdateVisual()
    {
        GameManager.instance.ammoCount.SetAmmoCount(ammoCount);
        GameManager.instance.ammoCount.SetMaxAmmoCount(maxAmmo);
    }

}
