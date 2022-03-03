using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject player;
    public GameObject force;
    public CharacterController p_controller;
    bool move = false;
    public bool teleport;
    public bool once;
    // Start is called before the first frame update
    void Start()
    {
        p_controller = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (teleport && !once)
        {
            GetComponent<Rigidbody>().AddForce(force.transform.forward*700);
            move = true;
            once = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (move)
        {
            //change player position 
            Debug.Log(player.transform.position);
            p_controller.enabled = false;
            player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            p_controller.enabled = true;
            Debug.Log(player.transform.position);
            this.gameObject.SetActive(false);
        }

    }
}
