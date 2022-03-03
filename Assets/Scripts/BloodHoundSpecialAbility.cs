using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BloodHoundSpecialAbility : MonoBehaviour
{
    // Start is called before the first frame update
    List<GameObject> allEnemies;
    public bool startSpecial = false;


    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fps;
    GameObject activeBloodHoundSpeciality;
    float startTime = 0.5f;
    int index = 0;
    InventorySystem inventorysystem;
    void Start()
    {
        List<GameObject> temp = GameObject.FindGameObjectsWithTag("Hero").ToList();
        allEnemies = temp.Concat(GameObject.FindGameObjectsWithTag("Champion").ToList()).ToList();

        fps = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        activeBloodHoundSpeciality = new GameObject("bloodHoundisActive");
        inventorysystem = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //change this line to be only activated when the player can activate the special ability
        if (startSpecial)
        {
            startTime += Time.deltaTime;

            if (startTime >= 0.1)
            {
                GameObject enemy = allEnemies[index++];
                if (index == allEnemies.Count) index = 0;
                //UnityStandardAssets.Characters.FirstPerson.FirstPersonController.bloodHoundActiveNow = true;
                startTime = 0;
                //gameObject.transform.parent = null;

                if (enemy != null)
                {
                    Vector3 enemyPos = enemy.transform.position;
                    float magnitude = (enemyPos - gameObject.transform.position).magnitude;
                    if (magnitude <= 10)
                    {
                        rotatePlayer(enemy);

                    }
                }
            }
        }
        else
        {
            startTime = 0.5f;
        }



    }


    void rotatePlayer(GameObject enemy)
    {

        fps.rotateViewMyImplementation(enemy);
        inventorysystem.shootWhatEverWeapon(enemy);



    }
}