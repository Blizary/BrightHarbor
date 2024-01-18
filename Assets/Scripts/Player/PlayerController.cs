using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum FacingDirection
{
    UP,
    DOWN,
    LEFT, 
    RIGHT
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FacingDirection currentdirection;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float health;
    [SerializeField] private float currentHealth;
    [SerializeField] private float invunerabilityTime;
    [SerializeField] private float lightAmount;
    [SerializeField] private float currentLightAmount;
    [SerializeField] private float dashPower;


    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 3f;
    [SerializeField] private float rateOfChange = 0.5f;

    [SerializeField] private List<LightToPlayerSpeedRelation> lightToPlayerSpeedRelations;

    private float currentValue = 0f;
    private float swordSummontimer = 0f;

    private float stealLightTimer = 0f;

    private bool mousePressed = false;

    [SerializeField] private GameEventListener_Float onPlayerHit;
    [SerializeField] private GameEvent onPlayerDeath;
    [SerializeField] private GameEvent_Integer onAttackTrigger;
    [SerializeField] private GameEvent_Integer onSummonSword;
    [SerializeField] private GameEvent onStealLight;
    [SerializeField] private GameEventListener_Float onEffectLight;

    private bool canBeHit = true;
    private bool isAlive = true;
    private bool isDashing = false;

    Animator animator;
    Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = health;
        //currentLightAmount = lightAmount;
        onPlayerHit.Response.AddListener(OnPlayerHit);
        UpdateSpeedBasedOnLight();
        onEffectLight.Response.AddListener(OnEffectLight);
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
            Dash();
            SummonSword();
            LightSteal();
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
        if(!isDashing)
        {
            transform.position += movement * moveSpeed * Time.deltaTime;
        }

        currentdirection = GetLastDirection(horizontalInput, verticalInput);
    }

    FacingDirection GetLastDirection(float horizontalInput, float verticalInput)
    {
        FacingDirection lastDirection = currentdirection;

        if (horizontalInput > 0)
        {
            lastDirection = FacingDirection.RIGHT;
        }
        else if (horizontalInput < 0)
        {
            lastDirection = FacingDirection.LEFT;
        }
        else if (verticalInput > 0)
        {
            lastDirection = FacingDirection.UP;
        }
        else if (verticalInput < 0)
        {
            lastDirection = FacingDirection.DOWN;
        }
        else
        {
            // No input, maintain the current direction or set to a default value
            //lastDirection = FacingDirection.DOWN;
        }

        return lastDirection;
    }

    void Attack()
    {
        if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (!mousePressed)
            {
                mousePressed = true;
                switch (currentdirection)
                {
                    case FacingDirection.UP:
                        onAttackTrigger.Raise(0);
                        break;
                    case FacingDirection.DOWN:
                        onAttackTrigger.Raise(1);
                        break;
                    case FacingDirection.RIGHT:
                        onAttackTrigger.Raise(2);
                        break;
                    case FacingDirection.LEFT:
                        onAttackTrigger.Raise(3);
                        break;
                }
                
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


    void SummonSword()
    {
        if(currentLightAmount >=1)
        {
            if (Input.GetKey(KeyCode.E))
            {
                // Key is being held down
                swordSummontimer += Time.deltaTime;
                animator.SetBool("SummoningSword", true);
                // Adjust the float value based on the timer
                currentValue = Mathf.Lerp(minValue, maxValue, swordSummontimer * rateOfChange);
            }
            else
            {
                // Key is not being pressed, reset the timer
                if (swordSummontimer > 0)
                {
                    Debug.Log(swordSummontimer);
                    if (swordSummontimer >= 1)
                    {
                        if (currentLightAmount >= (int)swordSummontimer)
                        {
                            onSummonSword.Raise((int)swordSummontimer);
                            UpdateLightAmount((int)swordSummontimer);

                        }

                    }
                }
                animator.SetBool("SummoningSword", false);
                swordSummontimer = 0f;

            }
        }
        

       
       
    }

    void Dash()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (currentLightAmount >= 1 && !isDashing)
            {
                UpdateLightAmount(-1);
                isDashing = true;
                StartCoroutine(IEDash());
            }
        }
       
    }

    IEnumerator IEDash()
    {
        Vector3 dir = new Vector3(0, 0, 0);
        switch(currentdirection)
        {
            case FacingDirection.UP:
                dir = new Vector3(0, 1, 0);
                break;
            case FacingDirection.DOWN:
                dir = new Vector3(0, -1, 0);
                break;
            case FacingDirection.RIGHT:
                dir = new Vector3(1, 0, 0);
                break;
            case FacingDirection.LEFT:
                dir = new Vector3(-1, 0, 0);
                break;
        }
        rb.AddForce(dir * dashPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        isDashing = false;
    }

    void LightDash()
    {
       
    }

    void LightSteal()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            // Key is being held down
            stealLightTimer += Time.deltaTime;

            // Adjust the float value based on the timer
            currentValue = Mathf.Lerp(0, 2, stealLightTimer * rateOfChange);

            if (stealLightTimer >= 1)
            {
                onStealLight.Raise();
                stealLightTimer = 0;
            }
        }
        else
        {
            stealLightTimer = 0f;

        }
        
    }

    void UpdateLightAmount(float _amount)
    {
        currentLightAmount += _amount;
        if(currentLightAmount < 0)
        {
            //event flare
        }
        UpdateSpeedBasedOnLight();

    }

    void UpdateSpeedBasedOnLight()
    {
        Debug.Log("Check speed");
        foreach(LightToPlayerSpeedRelation l in lightToPlayerSpeedRelations)
        {
            if(currentLightAmount >= l.minLight && currentLightAmount < l.maxLight)
            {
                moveSpeed = l.speed;
                Debug.Log("current speed: " +moveSpeed);
            }
        }
    }

    void OnEffectLight(float _Amount)
    {
        float newLightLevel = currentLightAmount;
        newLightLevel += _Amount;

        if(newLightLevel < 0)
        {
            newLightLevel = 0;
        }
        else if(newLightLevel > lightAmount) 
        {
            newLightLevel = lightAmount;
        }

        currentLightAmount += newLightLevel;
        UpdateSpeedBasedOnLight();
    }
}
