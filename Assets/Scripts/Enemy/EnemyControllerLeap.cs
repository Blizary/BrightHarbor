using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerLeap : EnemyController
{
    [SerializeField] private float lungeForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead && !gamePaused)
        {
            if (canMove)
            {
                SimpleChasePlayer();
            }
        }
    }

    protected override void SimpleChasePlayer()
    {
        base.SimpleChasePlayer();
        CheckDistanceToPlayer();
    }


    void CheckDistanceToPlayer()
    {
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

    protected override void OnEnemyHit(int _damage)
    {
        base.OnEnemyHit(_damage);
        StartCoroutine(IEWaitForAttackReset());

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!dead)
        {

            if (other.CompareTag("PlayerBody"))
            {
                onPlayerHit.Raise(damageOnHit);
            }
        }

    }
}
