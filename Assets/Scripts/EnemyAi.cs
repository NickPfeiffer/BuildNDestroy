using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask isGround, isPlayer;

    public float health;
    public GameObject healthbar;
    
    public ThirdPersonCharacter character;

    public Transform weapon;

    void Start()
    {
        agent.updateRotation = false;
    }
    
    // Patrolling
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    // Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Awake()
    {
        player = GameObject.Find("FirstPersonPlayer").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            ResetWeaponPos();
            Patrolling();
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            ResetWeaponPos();
            Chasing();
        }

        if (playerInAttackRange && playerInSightRange)
        {
            Attacking();
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
    }
    
    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        // walkpoint reached, searches for new one automatically
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        
        weapon.localEulerAngles = new Vector3(0, 90, 0);
        weapon.transform.localPosition = new Vector3(0.2f, 1.2f, 0.13f);

        if (!alreadyAttacked)
        {
            gameObject.GetComponent<AudioSource>().Play();
            
            player.gameObject.GetComponent<PlayerMovement>().takeDamage(15f);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetWeaponPos()
    {
        weapon.localEulerAngles = new Vector3(0, 0, 67);
        weapon.transform.localPosition = new Vector3(0.1f, 1.28f, -0.1f);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void AiTakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        updateHealthbar();
    }

    private void updateHealthbar()
    {
        Vector3 oldScale = healthbar.transform.localScale;
        healthbar.transform.localScale = new Vector3(health / 100, oldScale.y, oldScale.y);
    }
}
