using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    private bool didTrigger=false;
    public int damageAmount = 1;
    private void Update()
    {
        Destroy(gameObject,0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.damage(damageAmount);
        }
        Destroy(gameObject);
    }
}
