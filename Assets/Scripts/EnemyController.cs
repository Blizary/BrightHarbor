using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameObject player;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private float lungeForce;
    [SerializeField] private float damageOnHit;

    [SerializeField] private int health;
    [SerializeField] private int currentHealth;

    [SerializeField] private GameEvent_Float onPlayerHit;
    [SerializeField] private GameEventListener_Integer onEnemyHit;

    private bool canMove = true;
    private bool dead = false;

    private Rigidbody2D rb;
    private void Awake()
    {
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        onEnemyHit.Response.AddListener(OnEnemyHit);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            if (canMove)
            {
                SimpleChasePlayer();
            }
        }
        
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!dead)
        {

            if (other.CompareTag("PlayerBody"))
            {
                onPlayerHit.Raise(damageOnHit);
            }
        }
        
    }


    void SimpleChasePlayer()
    {

        // Calculate the direction to the target
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Set only the z rotation to face the player
        transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Move towards the target
        transform.Translate(direction * movementSpeed * Time.deltaTime);

        // Check the distance to the target
        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

        // If the distance is less than or equal to the stopping distance, send a debug message
        if (distanceToTarget <= attackRange)
        {
            canMove = false;
            StartCoroutine(IEWaitForLundgeAttack());
            //LungdeAttack();

        }
    }

    void LungdeAttack()
    {
        m_Animator.SetTrigger("Attack");
        // Calculate the direction towards the player
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Apply force to lunge at the player
        rb.AddForce(direction * lungeForce, ForceMode2D.Impulse);

        // Optionally, you can add other actions when lunging, such as playing an animation or sound
        StartCoroutine(IEWaitForAttackReset());

    }

    IEnumerator IEWaitForLundgeAttack()
    {
        yield return new WaitForSeconds(0.2f);
        LungdeAttack();
    }
    IEnumerator IEWaitForAttackReset()
    {
        yield return new WaitForSeconds(1.5f);
        rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
        canMove = true;
    }

    void OnEnemyHit(int _damage)
    {
        rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
        canMove = false;
        m_Animator.SetTrigger("Hit");
        UpdateHealth(-_damage);
        StartCoroutine(IEWaitForAttackReset());
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
