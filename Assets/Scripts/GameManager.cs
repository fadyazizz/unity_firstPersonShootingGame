using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    string character;
    public Canvas pauseMenu;
    public Canvas gameOverMenu;
    bool paused = false;
    bool gameOver = false;
    bool gameOver2 = false;
    bool win = false;
    bool win2 = false;
    public static GameManager inst;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public int maxAmmo;
    public int currentAmmo;
    public AmmoBar ammoBar;
    public int maxSecondAmmo;
    public int currentSecondAmmo;
    public SecondAmmoBar secondAmmoBar;
    public int maxPrimary;
    public int currentPrimary;
    public PrimaryBar primaryBar;
    public int maxSpecial = 100;
    public int currentSpecial;
    public SpecialAbilityBar specialBar;
    public MouseLook mouse;
    public Text weaponSelected;
    public Text secondWeaponSelected;
    public Text enemiesShotText;
    public int enemiesShot;

    float specialTimeRefill = 0;
    float specialTime = 1200;
    bool specialReady = false;
    bool specialOn = false;
    float cameraPosZ = 2.73f;
    float rotY = 180f;
    public GameObject lobaModel;
    public GameObject bloodHoundModel;
    public GameObject bangalorModel;
    public GameObject playerModel;
    public Animator bangalorAnim;
    public Animator lobaAnim;
    public Animator bloodHoundAnim;
    public GameObject cam;
    public GameObject camFinal;
    public float waitTime = 200000;
    public float wait2Time = 100000;
    public GameObject playerShield;
    bool shieldOn = false;
    public GameObject thrown;
    bool thrownCreated = false;
    bool gameOverPlayed = false;
    bool winPlayed = false;
    GameObject player;
    bool overMenu = false;

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Stop("MainMenu");
        AudioManager.instance.Play("Combat");
        character = PlayerPrefs.GetString("character");
        lobaModel.SetActive(false);
        bangalorModel.SetActive(false);
        bloodHoundModel.SetActive(false);
        //playerModel.SetActive(false);
        camFinal.SetActive(false);
        playerShield.SetActive(false);
        thrown.SetActive(false);
        enemiesShotText.text = "Enemies Shot: 0";
        player = GameObject.Find("player");
        if (character == "Loba")
        {
            maxSecondAmmo = 10;
            secondAmmoBar.SetMaxAmmo(10);
        }
        else
        {
            maxSecondAmmo = 5;
            secondAmmoBar.SetMaxAmmo(5);
        }
        if (character == "BloodHound")
        {
           
            player.AddComponent<BloodHoundSpecialAbility>();
        }
        pauseMenu.enabled = false;
        gameOverMenu.enabled = false;
        currentHealth = maxHealth;
        currentAmmo = 0;
        healthBar.SetMaxHealth(maxHealth);
        ammoBar.SetMaxAmmo(0);
        ammoBar.SetAmmo(0, 0);
        specialBar.SetMaxSpecial(100);
        currentSpecial = 0;
        setCurrentSpecial(currentSpecial);
        weaponSelected.text = "None";

    }

    public void SpecialRefill()
    {
        if (currentSpecial < 100)
        {
            // specialTimeRefill += Time.deltaTime;
            specialTimeRefill++;
            if (specialTimeRefill % 60 == 0)
            {
                currentSpecial += 5;
                setCurrentSpecial(currentSpecial);
            }
            
        }
        else
        {
            specialReady = true;
            specialTimeRefill = 0;
        }
    }

    public void StartSpecial()
    {
        specialOn = true;
        if (specialTime > 0)
        {
            //specialTime -= Time.deltaTime;
            specialTime--;
            if (specialTime % 60 == 0)
            {
                if (currentSpecial - 10 < 0)
                {
                    specialOn = false;
                    specialReady = false;
                    return;
                }
                currentSpecial -= 10;
                setCurrentSpecial(currentSpecial);
            }
        }
        else
        {
            specialTime = 1200;
            specialOn = false;
            specialReady = false;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
        paused = true;
        mouse.SetCursorLock(false);
        AudioManager.instance.Stop("Combat");
        AudioManager.instance.Play("MainMenu");
    }

    public void IncrementKills()
    {
        enemiesShot++;
        enemiesShotText.text = "Enemies Killed: " + enemiesShot.ToString();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        paused = false;
        mouse.SetCursorLock(true);
        AudioManager.instance.Stop("MainMenu");
        AudioManager.instance.Play("Combat");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        AudioManager.instance.Stop("MainMenu");
    }

    public void MainMenu()
    {
        mouse.SetCursorLock(false);
        AudioManager.instance.Stop("Parkour");
        AudioManager.instance.Stop("Combat");
        SceneManager.LoadScene("MainMenu");

    }
   

    // Update is called once per frame
    void Update()
    {
        if (paused == false)
        {
            pauseMenu.enabled = false;
        }
        else
        {
            pauseMenu.enabled = true;
        }

        if (overMenu == false)
        {
            gameOverMenu.enabled = false;
        }
        else
        {
            gameOverMenu.enabled = true;
        }

        if (gameOver == true)
        {
            camFinal.SetActive(true);
            playerModel.SetActive(false);
            if (character == "Loba")
            {
                lobaModel.SetActive(true);
                bangalorModel.SetActive(false);
                bloodHoundModel.SetActive(false);
                lobaAnim.SetBool("lose", true);
               
            }
            else if (character == "BloodHound")
            {
                bloodHoundModel.SetActive(true);
                lobaModel.SetActive(false);
                bangalorModel.SetActive(false);
                bloodHoundAnim.SetBool("lose", true);
              
            }
            else
            {
                bloodHoundModel.SetActive(false);
                lobaModel.SetActive(false);
                bangalorModel.SetActive(true);
                bangalorAnim.SetBool("lose", true);
            }
            gameOver2 = true;   
        }
        if (gameOver2)
        {
            if (waitTime > 0)
            {
                waitTime--;
                //waitTime -= Time.deltaTime;
            }
            else
            { 
                GameOver();
            }
        }
        if (enemiesShot == 23)
        {
            win = true;
            if (!winPlayed)
            {
                AudioManager.instance.Stop("PlayerFoot");
                AudioManager.instance.Stop("CoreAbility");
                AudioManager.instance.Stop("DeadEnemy");
                AudioManager.instance.Stop("Defensive");
                AudioManager.instance.Stop("EnemyHit");
                AudioManager.instance.Stop("FiringBullets");
                AudioManager.instance.Stop("PlayerHit");
                AudioManager.instance.Stop("Teleport");
                AudioManager.instance.Stop("WeaponPickup");
                AudioManager.instance.Stop("WeaponSwitch");
                AudioManager.instance.Stop("ZoneIn");
                AudioManager.instance.Stop("ZoneOut");
                AudioManager.instance.Play("Win");
                winPlayed = true;
            }
        }
        if (win == true)
        {
            camFinal.SetActive(true);
            playerModel.SetActive(false);
            if (character == "Loba")
            {
                lobaModel.SetActive(true);
                bangalorModel.SetActive(false);
                bloodHoundModel.SetActive(false);
                lobaAnim.SetBool("win", true);

            }
            else if (character == "BloodHound")
            {
                bloodHoundModel.SetActive(true);
                lobaModel.SetActive(false);
                bangalorModel.SetActive(false);
                bloodHoundAnim.SetBool("win", true);

            }
            else
            {
                bloodHoundModel.SetActive(false);
                lobaModel.SetActive(false);
                bangalorModel.SetActive(true);
                bangalorAnim.SetBool("win", true);
            }
            win2 = true;
        }
            if (win2)
        {
            if (wait2Time > 0)
            {
                //wait2Time -= Time.deltaTime;
                wait2Time--;
            }
            else
            {
                Win();
            }
        }

        if (currentHealth <= 0)
        {
            gameOver = true;
            if (!gameOverPlayed)
            {
                AudioManager.instance.Stop("PlayerFoot");
                AudioManager.instance.Stop("CoreAbility");
                AudioManager.instance.Stop("DeadEnemy");
                AudioManager.instance.Stop("Defensive");
                AudioManager.instance.Stop("EnemyHit");
                AudioManager.instance.Stop("FiringBullets");
                AudioManager.instance.Stop("PlayerHit");
                AudioManager.instance.Stop("Teleport");
                AudioManager.instance.Stop("WeaponPickup");
                AudioManager.instance.Stop("WeaponSwitch");
                AudioManager.instance.Stop("ZoneIn");
                AudioManager.instance.Stop("ZoneOut");
                AudioManager.instance.Play("DeadPlayer");
                gameOverPlayed = true;
            }
            
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
        if (!specialReady)
        {
            SpecialRefill();
        }
        if (Input.GetKeyDown(KeyCode.Q) && specialReady)
        {
            StartSpecial();
        }
        if (specialOn)
        {
            StartSpecial();
        }
        if (character == "Loba" && specialOn && !thrownCreated)
        {
            //keep Loba's special ability on
            //Debug.Log("loba");
            //Vector3 playerPos = new Vector3(cam.transform.position.x, cam.transform.position.y - 1.1f, cam.transform.position.z + 1f);
            //GameObject clone = Instantiate(thrown, playerPos, Quaternion.identity);
            //clone.transform.parent = cam.transform;
            //clone.SetActive(true);
            thrown.SetActive(true);
            Teleportation t = thrown.GetComponent<Teleportation>();
            AudioManager.instance.Play("Teleport");
            t.teleport = true;
            thrownCreated = true;
        }
        if (character == "Bangalor" && specialOn)
        {
            //keep Bangalor's special ability on
            shieldOn = true;
            playerShield.SetActive(true);
            AudioManager.instance.Play("Defensive");
        }
        if (character == "BloodHound" && specialOn)
        {
            //keep BloodHound's special ability on

            AudioManager.instance.Play("CoreAbility");
            BloodHoundSpecialAbility b = player.GetComponent<BloodHoundSpecialAbility>();
            b.startSpecial = true;
            
        }
        if (!specialOn)
        {
            if (character == "Loba")
            {
                thrownCreated = false;
                thrown.SetActive(false);
                Teleportation t = thrown.GetComponent<Teleportation>();
                t.once = false;
            }
            if (character == "Bangalor")
            {
                shieldOn = false;
                playerShield.SetActive(false);
                AudioManager.instance.Stop("Defensive");
            }
            if (character == "BloodHound")
            {
                BloodHoundSpecialAbility b = player.GetComponent<BloodHoundSpecialAbility>();
                b.startSpecial = false;

            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        mouse.SetCursorLock(false);
        gameOverMenu.enabled = true;
        overMenu = true;
        
        AudioManager.instance.Stop("DeadPlayer");
        AudioManager.instance.Stop("Combat");
        AudioManager.instance.Play("MainMenu");
        
        
    }
    public void Win()
    {
        AudioManager.instance.Stop("Win");
        LoadScene("Parkour");
    }
    public void LoadScene(string scene)
    {
        if (scene == "Parkour")
        {
            AudioManager.instance.Stop("Combat");
            AudioManager.instance.Play("Parkour");
            PlayerPrefs.SetString("character", "BloodHound");
        }
        SceneManager.LoadScene(scene);
    }

    public void TakeDamage(int damage)
    {
        if (!shieldOn)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        
    }

    public void setMaxPrimary(int maxPrimaryAmmo)
    {

        maxPrimary = maxPrimaryAmmo;
        primaryBar.SetMaxAmmo(maxPrimary);
    }
    public void setCurrentPrimary(int currentAmmo)
    {
        primaryBar.SetAmmo(currentAmmo, maxPrimary);
    }


    public void setCurrentAmmo(int currentAmmo)
    {
        ammoBar.SetAmmo(currentAmmo, maxAmmo);
    }
    public void setCurrentSecondAmmo(int currentAmmo)
    {
        secondAmmoBar.SetAmmo(currentAmmo, maxSecondAmmo);
    }

    public void setCurrentSpecial(int currentSpecial)
    {
        specialBar.SetSpecial(currentSpecial);
    }

    public void setWeapon(string name)
    {
        weaponSelected.text = name;
    }
    public void setSecondWeapon(string name)
    {
        secondWeaponSelected.text = name;
    }
}
