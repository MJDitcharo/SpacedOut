using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public enum WeaponID { Pistol, Shotgun, Heavy, Rifle, Melee };
    [SerializeField]
    private static bool weaponOnStart = false;
    public WeaponID weaponID { get; protected set; }
    [SerializeField]
    public Transform[] firePoint;
    public int firePointIndex = 0;
    [SerializeField]
    public GameObject bulletPrefab; //instance of bullet prefab

    [SerializeField]
    protected float fireRate = 7f; //firerate
    [SerializeField]
    public float bulletForce = 20f;
    [SerializeField]
    public int ammoCount; //ammo count 
    [SerializeField]
    public int maxAmmo;
    [SerializeField] protected float damage = 10; //damage
    [SerializeField] protected float damageMultiplier = 1;
    [SerializeField] protected float fireRateMultiplier = 1;
    [SerializeField] protected TMPro.TextMeshProUGUI ammoCountText;
    protected float nextShotFired = 0f; //counter for next bullet that is fired
    public string weaponDescription;
    public string weaponName;

    [SerializeField] public AudioClip gunshotAudio;
    [HideInInspector] public Sound gunshotSound;
    protected void Start()
    {
        UpdateVisual();

        gunshotSound = new Sound();
        gunshotSound.audio = gunshotAudio;
        gunshotSound.audioType = AudioStyle.sfx;
    }
    private void OnEnable()
    {
        UpdateVisual();
    }

    public virtual void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShotFired && ammoCount != 0 && Time.timeScale > 0) //if the first mouse button is down
        {
            nextShotFired = Time.time + 1f / fireRate / fireRateMultiplier; //delay for the next bullet fired
            Shoot(); //shoot method
        }
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint[firePointIndex].position, firePoint[firePointIndex].rotation); //spawn the bullet and reference the bullet to modify 
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); //acess the rigidbody of the game object
        bullet bulletScript = bullet.GetComponent<bullet>();
        bulletScript.damage = (int)(damage * damageMultiplier);
        rb.AddForce(firePoint[firePointIndex].forward * bulletForce, ForceMode.Impulse); //add a force in the up vector

        //deplete ammo
        ammoCount--;
        UpdateVisual();
        AudioManager.Instance.PlaySFX(gunshotSound);
    }

    public void AddAmmo(int ammo = System.Int32.MaxValue)
    {
        if (ammo == System.Int32.MaxValue)
            ammoCount++;
        else if (ammo + ammoCount > maxAmmo)
            ammoCount = maxAmmo;
        else
            ammoCount += ammo;
        UpdateVisual();
    }

    public virtual void SetActive(bool isActive) //set current weapon to sctive
    {
        gameObject.SetActive(isActive);
    }

    public void UpdateVisual()
    {
        ammoCountText.text = ammoCount.ToString();
    }

    public void SetDamageMultiplier(float multiplier)
    {
        damageMultiplier = multiplier;
    }

    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }

    public void SetFireRateMultiplier(float multiplier)
    {
        fireRateMultiplier = multiplier;
    }

    public float GetFireRateMultiplier()
    {
        return fireRateMultiplier;
    }

    public virtual void OnWeaponSwitch()
    {
        Debug.Log("Switched");
    }
}
