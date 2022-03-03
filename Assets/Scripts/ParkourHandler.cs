using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class ParkourHandler : MonoBehaviour
{
    public GameObject player;
    private string playername;
    
    public Canvas creditsCanvas;
    bool goal = false;
    public MouseLook mouse;
    // Start is called before the first frame update
    void Start()
    {
        //Get a handle to the Player
        player = GameObject.FindGameObjectWithTag("Player");
        playername = player.transform.name;
        creditsCanvas.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        //If we are inside the zone, all is good!
        if (col.transform.name == playername)
        {
            goal = true;
        }
    }

   
    // Update is called once per frame
    void Update()
    {

        if (goal == true)
        {
            mouse.SetCursorLock(false);
            creditsCanvas.enabled = true;
        }
    }
}
