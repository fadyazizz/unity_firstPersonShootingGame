
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;

    // is automatic gun
    public bool isAuto=false;

    //camera reference
    public Camera fpsCam;


    //muzzle flash
    public ParticleSystem muzzleFlash;


    // impact force when an object is being shot at
    public float impactForce=10f;


    //fire rate how fast can we fire our gun perSecond
    public float fireRate = 10;
    private float nextTimeToFire = 0f;

    //magazine Size
    public int magazineSize;
    // number of bullets left in the gun
    public int bulletsLeft=0;

    //Time taken to complete the reloading
    public float timeToReload = 2f;
    private float timeToShoot=0f;
    // temp Variable for testing

    public bool isEnemy = false;

    //players inventory system
    InventorySystem myInventory;
    private void Start()
    {
        myInventory = fpsCam.transform.parent.GetComponent<InventorySystem>();
        if (this.tag == "PrimaryWeapon")
        {
            GameManager.inst.setMaxPrimary(GetComponent<Gun>().magazineSize);
        }
        //GameManager.inst.setMaxAmmo(GetComponent<Gun>().magazineSize);
        print(GetComponent<Gun>().magazineSize);
        print(myInventory.name);
    }

    void Update()
    {
        if (!isEnemy)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                reload();
            }
            if (bulletsLeft == 0 || Time.time <= timeToShoot)
            {
                return;
            }
            if (isAuto && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                shoot();
            }
            else
            {
                if (!isAuto && Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    shoot();
                }
            }
        }
    }

    void shoot()
    {

        muzzleFlash.Play();
        RaycastHit hit;
        int layerMask = 1 << 7;
        bulletsLeft--;
        AudioManager.instance.Play("FiringBullets");
        if (this.tag == "PrimaryWeapon")
        {
            GameManager.inst.setCurrentPrimary(bulletsLeft);
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range,layerMask))
        {
            //insert logic to decrease health
            print("hit: "+hit.collider.name);
            Enemy enemyHit = hit.transform.GetComponent<Enemy>();
            if (enemyHit != null && gameObject.tag!="Player")
            {
                enemyHit.damage(damage);
            }
            addHitForce(hit);
        }
    }

    public void shootBloodHound()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //insert logic to decrease health
            print("hit: " + hit.collider.name);
            Enemy enemyHit = hit.transform.GetComponent<Enemy>();
            if (enemyHit != null && gameObject.tag != "Player")
            {
                enemyHit.damage(damage);
            }
            addHitForce(hit);
        }

    }

        public void shootEnemy()
    {
        print("shooting");
        muzzleFlash.Play();
        RaycastHit hit;
        GameObject enemy = gameObject.transform.parent.gameObject;
        if (Physics.Raycast(enemy.transform.position, enemy.transform.forward, out hit, range))
        {
            print("here" + hit.transform.gameObject.name);
            Enemy enemyHit = hit.transform.gameObject.GetComponent<Enemy>();
            //print(enemyHit.gameObject.name);
            if (enemyHit != null && hit.transform.gameObject.tag == "Player")
            {

                enemyHit.damage(damage);
            }
        }
    }

    void addHitForce(RaycastHit hit)
    {
        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }
    }
    void reload()
    {
        if (bulletsLeft == magazineSize)
        {
            return;
        }
        timeToShoot = timeToReload + Time.time;
        // insert logic to get bullets from inventory system
        int reloadSize = magazineSize - bulletsLeft;
        int currentPrimaryAmmo = myInventory.getPrimaryAmmo();
        print(currentPrimaryAmmo);
        int numberOfBulletsAvaliable = currentPrimaryAmmo - reloadSize <= 0 ? currentPrimaryAmmo : reloadSize;
        bulletsLeft += numberOfBulletsAvaliable;


        if (this.tag == "PrimaryWeapon")
        {
            GameManager.inst.setCurrentPrimary(bulletsLeft);
        }
        int left = currentPrimaryAmmo - reloadSize <= 0 ? 0 : currentPrimaryAmmo - reloadSize;
        myInventory.setPrimaryAmmo(left);


    }

}
