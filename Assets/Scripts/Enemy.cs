
using UnityEngine;

public class Enemy : MonoBehaviour
{


    float health = 100f;


    public void damage(int damageValue) {
       
        if (gameObject.CompareTag("Player"))
        {
            GameManager.inst.TakeDamage(damageValue);
            AudioManager.instance.Play("PlayerBlood");
            AudioManager.instance.Play("PlayerHit");
            
        }
        else if (gameObject.CompareTag("Hero"))
        {
            Debug.Log("ba2tlo");
            Hero h = gameObject.GetComponent<Hero>();
            h.health = h.health - damageValue;
            AudioManager.instance.Play("EnemyBlood");
            AudioManager.instance.Play("EnemyHit");
            
            Animator cA = gameObject.GetComponent<Animator>();
            cA.SetBool("patrol", false);
            cA.SetBool("attack", false);
            cA.SetBool("hit", true);
            cA.SetBool("sprint", false);
        }
        else if (gameObject.CompareTag("Champion"))
        {
            Champion c = gameObject.GetComponent<Champion>();
            c.health = c.health - damageValue;
            AudioManager.instance.Play("EnemyHit");
            Animator cA = gameObject.GetComponent<Animator>();
            cA.SetBool("patrol", false);
            cA.SetBool("attack", false);
            cA.SetBool("hit", true);
            cA.SetBool("sprint", false);

        }
        //health -= damageValue;
        //print(gameObject.name + " " + health);
        //if (health <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    print(other.gameObject.name);
    //    Destroy(other.gameObject);
    //    int damageAmount=other.gameObject.GetComponent<ObjectDetector>().damageAmount;
    //    health -= damageAmount;
    //    if (health <= 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
