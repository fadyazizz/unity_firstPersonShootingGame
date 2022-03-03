using UnityEngine;


public class GunfireController : MonoBehaviour
{


    // --- Muzzle ---
    public GameObject muzzlePrefab;
    public GameObject muzzlePosition;
    bool canShoot = false;


    public float shotDelay = .5f;

    //launching angle of the grenade
    public Transform projectileInitialDirection;

    // the intial speed of the grenade launched
    public float initialSpeed;



    //reference to the inventory system
    InventorySystem myInventory;

    // --- Projectile ---
    [Tooltip("The projectile gameobject to instantiate each time the weapon is fired.")]
    public GameObject projectilePrefab;
    [Tooltip("Sometimes a mesh will want to be disabled on fire. For example: when a rocket is fired, we instantiate a new rocket, and disable" +
        " the visible rocket attached to the rocket launcher")]
    public GameObject projectileToDisableOnFire;

    // --- Timing ---
    [SerializeField] private float timeLastFired;


    private void Start()
    {

        timeLastFired = 0;
        myInventory = GameObject.Find("player").GetComponent<InventorySystem>();
    }

    private void Update()
    {
        
        if (!areThereGrenades())
        {
            return;
        }
        
        // --- Fires the weapon if the delay time period has passed since the last shot ---
        if (Input.GetButtonDown("Fire1") && ((timeLastFired + shotDelay) <= Time.time) && canShoot)
        {
            FireWeapon();
            canShoot = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            canShoot = true;
        }
    }

    /// <summary>
    /// Creates an instance of the muzzle flash.
    /// Also creates an instance of the audioSource so that multiple shots are not overlapped on the same audio source.
    /// Insert projectile code in this function.
    /// </summary>
    public void FireWeapon()
    {
        // --- Keep track of when the weapon is being fired ---
        timeLastFired = Time.time;


        int grenadesNumber = myInventory.getSecondaryAmmo();
        myInventory.setSecondaryAmmo(grenadesNumber - 1);
        // --- Spawn muzzle flash ---
        var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);

        // --- Shoot Projectile Object ---
        if (projectilePrefab != null)
        {

            GameObject newProjectile = Instantiate(projectilePrefab, muzzlePosition.transform.position, muzzlePosition.transform.rotation);
            Rigidbody grenadeRigidBody = newProjectile.GetComponent<Rigidbody>();
            grenadeRigidBody.AddForce(projectileInitialDirection.forward * initialSpeed);
        }

        // --- Disable any gameobjects, if needed ---
        if (projectileToDisableOnFire != null)
        {
            projectileToDisableOnFire.SetActive(false);
            Invoke("ReEnableDisabledProjectile", 3);
        }


        // GameObject grenadeInstantiated = Instantiate(grenade, projectileInitialDirection.position, Quaternion.identity);



    }
    bool areThereGrenades()
    {
        //handle logic to get the number of secondary ammo
        if (myInventory.getSecondaryAmmo() <= 0)
        {
            return false;
        }
        return true;
    }

    private void ReEnableDisabledProjectile()
    {

        projectileToDisableOnFire.SetActive(true);
    }

    public void BloodHoundFire(GameObject enemy)
    {
        var flash = Instantiate(muzzlePrefab, muzzlePosition.transform);
        if (projectilePrefab != null)
        {
            Vector3 enemyPosition = enemy.transform.position;

            GameObject newProjectile = Instantiate(projectilePrefab, enemyPosition, muzzlePosition.transform.rotation);
            Rigidbody grenadeRigidBody = newProjectile.GetComponent<Rigidbody>();
            grenadeRigidBody.AddForce(enemy.transform.forward * 1);
        }
        if (projectileToDisableOnFire != null)
        {
            projectileToDisableOnFire.SetActive(false);
            Invoke("ReEnableDisabledProjectile", 3);
        }
    }
}