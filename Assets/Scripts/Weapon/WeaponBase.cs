using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Transform firePoint;
    [SerializeField]
    public GameObject bulletPrefab; //instance of bullet prefab

    [SerializeField]
    protected float fireRate = 7f; //firerate
    [SerializeField]
    public float bulletForce = 20f;
    [SerializeField]
    protected int ammoCount; //ammo count 
    protected float damage; //damage
    AmmoCount ammoCountVisual;
    protected float nextShotFired = 0f; //counter for next bullet that is fired
                                        // Update is called once per frame

    private void Awake()
    {
        //grab ammo for UI
        ammoCountVisual = GameManager.instance.ammoCount;
        ammoCountVisual.SetAmmoCount(ammoCount);
    }

    public virtual void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShotFired && ammoCount != 0) //if the first mouse button is down
        {
            nextShotFired = Time.time + 1f / fireRate; //delay for the next bullet fired
            Shoot(); //shoot method
            ammoCount--;
            ammoCountVisual.SetAmmoCount(ammoCount);
        }
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); //add a force in the up vector

    }

    public virtual void SetActive(bool isActive) //set current weapon to sctive
    {
        gameObject.SetActive(isActive);
    }

}
