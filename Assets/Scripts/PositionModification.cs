using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionModification : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject cmaerRef;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 cameraPos = camera.transform.eulerAngles;
        // gameObject.transform.eulerAngles = new Vector3(-cameraPos.x, cameraPos.y, cameraPos.z);
        // gameObject.transform.eulerAngles = new Vector3(0,player.transform.eulerAngles.y,0);
        gameObject.transform.eulerAngles = new Vector3(cmaerRef.transform.eulerAngles.x, player.transform.eulerAngles.y, 0);



    }
}