using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public GameObject mainPlayer;


    public GameObject fpsCamera;
    GameObject primaryWeapon = null;
    GameObject secondaryWeapon = null;

    int maxPrimAmmo = 150;
    int maxSecAmmo = 5;
    [SerializeField]
    int currPrimAmmo = 0;
    [SerializeField]
    int currSecAmmo = 0;

    int incPrimAmmo = 50;
    int incSecAmmo = 2;


    public GameObject holdingAssultRifle;
    public GameObject holdingSniper;
    public GameObject holdingShotgun;



    public GameObject floorAssultRifle;
    public GameObject floorSniper;
    public GameObject floorShotgun;

    private GameObject objectToBePickedUp;
    //prefab references to secondary weapons that teh player can hold
    public GameObject holdingGrenadeLauncher;
    public GameObject holdingFireGrenadeLauncher;

    // prefab references to Secondary weapons placeholder ( just the models with a trigger box collider with no shooting functionality)
    public GameObject floorGrenadeLauncher;
    public GameObject floorFireGrenadeLauncher;
    public Animator playerAnim;
    //public static InventorySystem inst;

    private void Awake()
    {
        //inst = this;
    }

    private void Start()
    {
        string character = PlayerPrefs.GetString("character");
        if (character == "Loba")
        {
            maxSecAmmo = 10;
        }
        else
        {
            maxSecAmmo = 5;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        objectToBePickedUp = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        objectToBePickedUp = null;
    }

    private void pickupInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && objectToBePickedUp != null)
        {
            //GameManager.inst.playerModel.SetActive(true);
            playerAnim.SetBool("pickUp", false);
            playerAnim.Rebind();
            playerAnim.SetBool("pickUp", true);
            //Invoke("CloseModel", 4);
            AudioManager.instance.Play("WeaponPickup");
            if (objectToBePickedUp.tag == "PrimaryWeapon")
            {
                Debug.Log(objectToBePickedUp.tag);
                handlePrimaryWeaponPickup(objectToBePickedUp);

            }
            if (objectToBePickedUp.tag == "SecondaryWeapon")
            {
                handleSecondaryWeaponPickup(objectToBePickedUp);

            }
            if (objectToBePickedUp.tag == "PrimaryAmmo")
            {
                print("in");
                handlePrimaryAmmoPickup(objectToBePickedUp);
            }
            if (objectToBePickedUp.tag == "SecondaryAmmo")
            {
                print("SecondaryAmmo");
                handleSecondaryAmmoPickup(objectToBePickedUp);
            }
        }

    }

    void CloseModel()
    {
        GameManager.inst.playerModel.SetActive(false);
    }
    private void weaponSwitchingInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (secondaryWeapon != null && primaryWeapon != null)
            {
                AudioManager.instance.Play("WeaponSwitch");
                if (secondaryWeapon.activeSelf)
                {
                    secondaryWeapon.SetActive(false);
                    primaryWeapon.SetActive(true);
                    return;
                }
                primaryWeapon.SetActive(false);
                secondaryWeapon.SetActive(true);
            }
        }
    }
    private void Update()
    {
        pickupInput();
        weaponSwitchingInput();
    }

    void sendWeaponName(string name)
    {
        if (name.Contains("Rifle_06"))
        {
            //assault rifle
            GameManager.inst.setWeapon("Assault Rifle");
        }
        if (name.Contains("Heavy_08"))
        {
            //shotgun
            GameManager.inst.setWeapon("Shotgun");
        }
        if (name.Contains("Sniper_04 1"))
        {
            //sniper rifle
            GameManager.inst.setWeapon("Sniper Rifle");
        }
    }

    void sendSecondWeaponName(string name)
    {

        if (name.Contains("fireGrenadeLauncher"))
        {
            //assault rifle
            GameManager.inst.setSecondWeapon("Flame Grenade Launcher");
        }
        if (name.Contains("grenadeLauncher"))
        {
            //shotgun
            GameManager.inst.setSecondWeapon("Grenade Launcher");
        }

    }

    private void handlePrimaryWeaponPickup(GameObject gameObject)
    {
        //primarypickup
        sendWeaponName(gameObject.name);
        if (primaryWeapon == null)
        {

            string name = gameObject.name;

            Destroy(gameObject);



        }
        else
        {
            Vector3 playerPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            if (primaryWeapon.name.Contains("Rifle_06(Clone)"))
            {
                // playerPos += new Vector3(0, floorAssultRifle.transform.position.y, 0);
                Instantiate(floorAssultRifle, playerPos, Quaternion.identity);
            }
            if (primaryWeapon.name.Contains("Heavy_08(Clone)"))
            {
                //playerPos += new Vector3(0, floorShotgun.transform.position.y, 0);
                Instantiate(floorShotgun, playerPos, Quaternion.identity);
            }
            if (primaryWeapon.name.Contains("Sniper_04 1(Clone)"))
            {
                // playerPos += new Vector3(0, floorSniper.transform.position.y, 0);
                Instantiate(floorSniper, playerPos, Quaternion.identity);
            }

            Destroy(primaryWeapon);
            string name = gameObject.name;

            Destroy(gameObject);



        }
        instantiateNewPrimaryWeapon(gameObject.name);

    }
    private void instantiateNewPrimaryWeapon(string name)
    {
        GameObject newWeapon = null;
        if (name.Contains(holdingAssultRifle.name))
        {

            newWeapon = Instantiate(holdingAssultRifle, fpsCamera.transform);
            newWeapon.transform.parent = fpsCamera.transform;
            Camera temp = fpsCamera.GetComponent<Camera>();
            newWeapon.GetComponent<Gun>().fpsCam = temp;
            primaryWeapon = newWeapon;

        }
        if (name.Contains(holdingShotgun.name))
        {
            newWeapon = Instantiate(holdingShotgun, fpsCamera.transform);
            newWeapon.transform.parent = fpsCamera.transform;
            Camera temp = fpsCamera.GetComponent<Camera>();
            newWeapon.GetComponent<Gun>().fpsCam = temp;
            primaryWeapon = newWeapon;
        }
        if (name.Contains(holdingSniper.name))
        {
            newWeapon = Instantiate(holdingSniper, fpsCamera.transform);
            newWeapon.transform.parent = fpsCamera.transform;
            Camera temp = fpsCamera.GetComponent<Camera>();
            newWeapon.GetComponent<Gun>().fpsCam = temp;
            primaryWeapon = newWeapon;
        }
        if (secondaryWeapon != null && secondaryWeapon.activeSelf)
        {
            newWeapon.SetActive(false);
        }

    }

    public void shootWhatEverWeapon(GameObject enemy)
    {
        if (primaryWeapon != null && primaryWeapon.activeSelf)
        {
            primaryWeapon.GetComponent<Gun>().shootBloodHound();
        }
        else
        {
            if (secondaryWeapon != null && secondaryWeapon.activeSelf)
            {
                secondaryWeapon.GetComponent<GunfireController>().BloodHoundFire(enemy);
            }
        }
    }
    private void handleSecondaryWeaponPickup(GameObject gameObject)
    {

        sendSecondWeaponName(gameObject.name);
        if (secondaryWeapon == null)
        {

            string name = gameObject.name;
            instantiateNewSecondaryWeapon(gameObject.name);
        }
        else
        {

            Vector3 playerPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            if (secondaryWeapon.name.Contains("fireGrenadeLauncher(Clone)"))
            {

                Instantiate(floorFireGrenadeLauncher, playerPos, Quaternion.identity);
            }
            if (secondaryWeapon.name.Contains("grenadeLauncher(Clone)"))
            {

                Instantiate(floorGrenadeLauncher, playerPos, Quaternion.identity);
            }


            Destroy(secondaryWeapon);
            string name = gameObject.name;


            instantiateNewSecondaryWeapon(gameObject.name);


        }
        Destroy(gameObject);

    }
    private void instantiateNewSecondaryWeapon(string name)
    {
        GameObject newWeapon = null;
        if (name.Contains(holdingFireGrenadeLauncher.name))
        {
            newWeapon = Instantiate(holdingFireGrenadeLauncher, fpsCamera.transform);
            newWeapon.transform.parent = fpsCamera.transform;

            secondaryWeapon = newWeapon;
        }
        if (name.Contains(holdingGrenadeLauncher.name))
        {
            newWeapon = Instantiate(holdingGrenadeLauncher, fpsCamera.transform);
            newWeapon.transform.parent = fpsCamera.transform;

            secondaryWeapon = newWeapon;
        }
        if (primaryWeapon != null && primaryWeapon.activeSelf)
        {
            newWeapon.SetActive(false);
        }
    }


    private void handlePrimaryAmmoPickup(GameObject gameObject)
    {
        if (currPrimAmmo + incPrimAmmo <= maxPrimAmmo)
        {
            //print(currPrimAmmo);
            currPrimAmmo += incPrimAmmo;
            GameManager.inst.setCurrentAmmo(currPrimAmmo);
            Destroy(gameObject);
            print(currPrimAmmo);
        }


    }

    private void handleSecondaryAmmoPickup(GameObject gameObject)
    {
        print("picking up secondary ammo");
        if (currSecAmmo + incSecAmmo <= maxSecAmmo)
        {
            currSecAmmo += incSecAmmo;
            GameManager.inst.setCurrentSecondAmmo(currSecAmmo);
            Destroy(gameObject);
        }

    }
    public int getPrimaryAmmo()
    {
        return currPrimAmmo;
    }
    public void setPrimaryAmmo(int valueAfterReloading)
    {
        currPrimAmmo = valueAfterReloading;
        GameManager.inst.setCurrentAmmo(currPrimAmmo);
    }
    public int getSecondaryAmmo()
    {
        return currSecAmmo;
    }

    public void setSecondaryAmmo(int valueAfterShooting)
    {
        currSecAmmo = valueAfterShooting;
        GameManager.inst.setCurrentSecondAmmo(valueAfterShooting);
    }

}