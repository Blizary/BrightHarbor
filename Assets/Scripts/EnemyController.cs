using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject player;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;

    [SerializeField] private int health;
    [SerializeField] private int currentHealth;

    private bool canMove = true;
    private bool dead = false;


    private void Awake()
    {
        currentHealth = health;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            SimpleChasePlayer();
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Sword"))
        {
            OnEnemyHit();
            
        }
    }


    void SimpleChasePlayer()
    {
        // Calculate the direction to the target
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Move towards the target
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // Check the distance to the target
        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

        // If the distance is less than or equal to the stopping distance, send a debug message
        if (distanceToTarget <= attackRange)
        {
            canMove = false;
            LungdeAttack();
            
        }
    }

    void LungdeAttack()
    {
        m_Animator.SetTrigger("Attack");
        // Calculate the direction to the target
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        // Move towards the target
        transform.Translate(direction * movementSpeed*300 * Time.deltaTime);

        
        StartCoroutine(WaitForAttackReset());
    }

    IEnumerator WaitForAttackReset()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    void OnEnemyHit()
    {
        m_Animator.SetTrigger("Hit");
        UpdateHealth(-1);
    }

    void UpdateHealth(int _diff)
    {
        if(!dead)
        {
            currentHealth += _diff;
            if (currentHealth <= 0)
            {
                m_Animator.SetTrigger("Death");
                canMove = false;
                dead = true;
            }

        }
          
    }
}
