using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBig : EnemyController
{
    private ShadowLvlCamController shadowlvlController;
    [SerializeField] private float attackResetTimer;
    [SerializeField] private float maxSpeed;
    private float normalSpeed;

    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        shadowlvlController = GameObject.FindFirstObjectByType<ShadowLvlCamController>();
        normalSpeed = movementSpeed;
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

        if (shadowlvlController.shadowlvl>=2)
        {
            //speed up
            movementSpeed = maxSpeed;
        }
        else
        {
            movementSpeed=normalSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!dead)
        {
            if (other.CompareTag("PlayerBody"))
            {
                onPlayerHit.Raise(damageOnHit);
                canAttack = false;
                canMove = false;
                m_Animator.SetBool("Wait", true);
                StartCoroutine(IERefreshAttack());
            }
        }

    }

    IEnumerator IERefreshAttack()
    {
        yield return new WaitForSeconds(attackResetTimer);
        canAttack = true;
        canMove = true;
        m_Animator.SetBool("Wait", false);
    }

    protected override void OnEnemyHit(int _damage)
    {
        rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
        m_Animator.SetTrigger("Hit");
        UpdateHealth(-_damage);

    }
}
