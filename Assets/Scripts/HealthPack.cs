using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.inst.currentHealth < 75)
            {
                GameManager.inst.currentHealth += 25;
                GameManager.inst.healthBar.SetHealth(GameManager.inst.currentHealth);
                Destroy(this.gameObject);
            }
        }
    }
}
