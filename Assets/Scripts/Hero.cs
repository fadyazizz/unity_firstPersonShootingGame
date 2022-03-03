using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Hero : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    public LayerMask whatIsGround, whatIsPlayer;

    public Transform player;

    public int health;
    public int maxHealth;
    public GameObject healthBarUI;
    public Slider slider;
    public Gun myGun;

    // Start is called before the first frame update

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    private float positionX;
    private float positionZ;


    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    bool dead;
    int waitTime = 200;
    bool recorded;


    void Start()
    {       positionX=transform.position.x;
            positionZ=transform.position.z;
            navMeshAgent = GetComponent<NavMeshAgent> ();

        health = maxHealth;
        slider.value = CalculateHealth();

    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    private void Update()
    {
        slider.value = CalculateHealth();
        if (health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health<=0)
        {
            GetComponent<Animator>().SetBool("patrol", false);
            GetComponent<Animator>().SetBool("attack", false);
            GetComponent<Animator>().SetBool("hit", false);
            GetComponent<Animator>().SetBool("sprint", false);
            GetComponent<Animator>().SetBool("dead", true);
            dead = true;
            if (!recorded)
            {
                GameManager.inst.IncrementKills();
                recorded = true;
                AudioManager.instance.Play("DeadEnemy");
            }
            //transform.position = new Vector3(positionX, -1.0f, positionZ);

        }
        else
        {
 //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
        if (dead)
        {
            if (waitTime > 0)
            {
                waitTime--;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        walkPoint = new Vector3(positionX + randomX, transform.position.y, positionZ + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void Patroling()
    {
        GetComponent<Animator>().SetBool("patrol", true);
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetBool("hit", false);
        GetComponent<Animator>().SetBool("sprint", false);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
            AudioManager.instance.Play2("EnemyFoot");

        }
            

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }


    private void ChasePlayer()
    {
        //GetComponent<Animator>().SetBool("patrol", true);

        navMeshAgent.SetDestination(player.position);
        GetComponent<Animator>().SetBool("patrol", false);
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetBool("hit", false);
        GetComponent<Animator>().SetBool("sprint", true);
        AudioManager.instance.Play2("EnemyFoot");
    }

    // Update is called once per frame

    //     private void AttackPlayer()
    // {
    //     Debug.Log("BOOM BOOM");
    // }

    private void AttackPlayer()
    {
        GetComponent<Animator>().SetBool("patrol", false);
        GetComponent<Animator>().SetBool("attack", true);
        GetComponent<Animator>().SetBool("hit", false);
        GetComponent<Animator>().SetBool("sprint", false);
        //Make sure enemy doesn't move
        navMeshAgent.SetDestination(transform.position);

        transform.LookAt(player);



        if (!alreadyAttacked)
        {
            ///Attack code here
            myGun.shootEnemy();

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

        public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

        private void DestroyEnemy()
    {
        Destroy(gameObject);
    }


}
