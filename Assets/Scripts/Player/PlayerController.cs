using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Adjust this to control the player's movement speed
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    [SerializeField] private float invunerabilityTime;
    [SerializeField] private GameEvent onAttackTrigger;
   
    private bool mousePressed = false;

    [SerializeField] private GameEventListener_Float onPlayerHit;
    [SerializeField] private GameEvent onPlayerDeath;

    private bool canBeHit = true;
    private bool isAlive = true;

    private void Awake()
    {
        currentHealth = health;
        onPlayerHit.Response.AddListener(OnPlayerHit);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Movement();
            Attack();
        }
    }

    void Movement()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;

        // Move the player
        transform.position += movement * moveSpeed * Time.deltaTime;

    }

    void Attack()
    {
        if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (!mousePressed)
            {
                mousePressed = true;
                onAttackTrigger.Raise();
            }

        }
        else
        {
            mousePressed = false;
        }
    }

    void OnPlayerHit(float _damage)
    {
        if(isAlive && canBeHit)
        {
            canBeHit = false;
            currentHealth -= _damage;
            if (currentHealth <= 0)
            {
                //its dead
                isAlive = false;
                onPlayerDeath.Raise();
            }
            else
            {
                //invunerability
                StartCoroutine(IEInvunerabilityTimer());
            }
        }
        
    }

    IEnumerator IEInvunerabilityTimer()
    {
        yield return new WaitForSeconds(invunerabilityTime);
        canBeHit = true;
    }
}
