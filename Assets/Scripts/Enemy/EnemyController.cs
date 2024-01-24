using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyController : MonoBehaviour
{
    [SerializeField] protected Animator m_Animator;
    [SerializeField] protected SpriteRenderer bodyRenderer;
    [SerializeField] protected GameObject player;
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float damageOnHit;
    [SerializeField] protected float lightAmountOnDeath;

    [SerializeField] protected int health;
    [SerializeField] protected int currentHealth;

    [SerializeField] protected GameEvent_Float onPlayerHit;
    [SerializeField] protected GameEventListener_Integer onEnemyHit;
    [SerializeField] protected GameEventListener_Bool onExitMenuShow;
    [SerializeField] protected GameEventListener_Bool onPauseMenuShow;
    [SerializeField] protected GameEvent_Float onAddLight;

    protected bool canMove = true;
    protected bool dead = false;
    [SerializeField] protected bool gamePaused = false;

    protected Rigidbody2D rb;
    private void Awake()
    {
        player = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        onEnemyHit.Response.AddListener(OnEnemyHit);
        onExitMenuShow.Response.AddListener(OnExitMenuShow);
        onPauseMenuShow.Response.AddListener(OnPauseMenuShow);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead && !gamePaused)
        {
            if (canMove)
            {
                SimpleChasePlayer();
            }
        }
        
        
    }


   


    protected virtual void SimpleChasePlayer()
    {

        // Calculate the direction to the target
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle = Mathf.Abs(angle);
        if(angle <= 90)
        {
            //player is on the left flip
            bodyRenderer.flipX = false;
        }
        else
        {
            //player is on the right
            bodyRenderer.flipX = true;

        }
        // Set only the z rotation to face the player
        //transform.GetChild(0).transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Move towards the target
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    


    protected virtual void OnEnemyHit(int _damage)
    {
        rb.AddForce(-rb.velocity, ForceMode2D.Impulse);
        canMove = false;
        m_Animator.SetTrigger("Hit");
        UpdateHealth(-_damage);
        
    }

    protected void UpdateHealth(int _diff)
    {
        if(!dead)
        {
            currentHealth += _diff;
            if (currentHealth <= 0)
            {
                m_Animator.SetTrigger("Death");
                onAddLight.Raise(lightAmountOnDeath);
                canMove = false;
                dead = true;

            }

        }
          
    }

    protected void OnExitMenuShow(bool _state)
    {
        if(_state)
        {
            gamePaused = true;
            
        }
        else
        {
            gamePaused = false;
        }
    }


    protected void OnPauseMenuShow(bool _state)
    {
        if (_state)
        {
            gamePaused = true;

        }
        else
        {
            gamePaused = false;
        }
    }



}
