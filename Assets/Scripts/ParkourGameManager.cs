using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class ParkourGameManager : MonoBehaviour
{
    public GameObject player;
    private string playername;

    public Canvas pauseMenu;
    public Canvas gameOverMenu;
    bool paused = false;
    bool gameOver = false;
    public MouseLook mouse;
    // Start is called before the first frame update
    void Start()
    {
        //Get a handle to the Player
        AudioManager.instance.Play("Parkour");
        player = GameObject.FindGameObjectWithTag("Player");
        playername = player.transform.name;
        pauseMenu.enabled = false;
        gameOverMenu.enabled = false;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
        paused = true;
        mouse.SetCursorLock(false);
        AudioManager.instance.Stop("Parkour");
        AudioManager.instance.Play("MainMenu");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        paused = false;
        mouse.SetCursorLock(true);
        AudioManager.instance.Stop("MainMenu");
        AudioManager.instance.Play("Parkour");
    }

    public void Restart()
    {
        AudioManager.instance.Stop("MainMenu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        mouse.SetCursorLock(false);
        AudioManager.instance.Stop("Parkour");
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

        if (gameOver == true)
        {
            mouse.SetCursorLock(false);
            gameOverMenu.enabled = true;
        }


        if (player.transform.position.y < -5)
        {
            gameOver = true;
            gameOverMenu.enabled = true;
        }

        if(Input.GetKey(KeyCode.Escape) && !Input.GetKey(KeyCode.Space))
        {
            Debug.Log("pause");
            PauseGame();
        }
    }
}
