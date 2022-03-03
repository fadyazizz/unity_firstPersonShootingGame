using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetectorFireGrenade : MonoBehaviour
{

    public int damageAmount = 25;
    float timer = 0;
    float decreaseHealthTimer = 0;
    List<GameObject> allObjsToDecreaseTheirDamage=new List<GameObject>();
    private void Start()
    {
        timer = Time.time;
        
    }
    private void Update()
    {
        if (Time.time - timer >= 5)
        {
            Destroy(gameObject, 0.1f);
        }
        decreaseHealthTimer += Time.deltaTime;
        if (decreaseHealthTimer >= 1)
        {
            decreaseHealthTimer = 0;
            decreaseHealth();
        }
    }
    private void decreaseHealth()
    {
        for(int i = 0; i < allObjsToDecreaseTheirDamage.Count; i++)
        {
            if (allObjsToDecreaseTheirDamage[i] == null)
            {
                continue;
            }
            Enemy enemy = allObjsToDecreaseTheirDamage[i].GetComponent<Enemy>();
            if (enemy != null )
            {
                enemy.damage(damageAmount);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        allObjsToDecreaseTheirDamage.Add(other.gameObject);   
    }
    private void OnTriggerExit(Collider other)
    {
       // print(other.gameObject.name);

        allObjsToDecreaseTheirDamage.Remove(other.gameObject);
       
       
    }

   
}
