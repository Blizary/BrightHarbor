using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarborControlls : MonoBehaviour
{
    [SerializeField] private FacingDirection currentdirection;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 3f;
    [SerializeField] private float rateOfChange = 0.5f;

    [SerializeField] private float currentRequiredGatherTimer = 1f;
    private float gatherMaterialTimer = 0f;

    private bool mousePressed = false;


    [SerializeField] private GameEvent_Bool onPauseMenuShow;
    [SerializeField] private GameEvent onGatherMaterial;
    [SerializeField] private GameEventListener_Float onGatherTimerUpdate;



    [SerializeField] private Animator bodyAnimator;

    private bool gamePaused = false;

    Animator animator;
    Rigidbody2D rb;
    UIController uiController;

    private void Awake()
    {
       // uiController = GameObject.FindFirstObjectByType<UIController>().GetComponent<UIController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //currentLightAmount = lightAmount;
       

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamePaused)
        {
            Movement();
            Gathermaterial();
        }
        PauseGame();
    }

    void Gathermaterial()
    {
        if (Input.GetKey(KeyCode.R))
        {
            // Key is being held down
            gatherMaterialTimer += Time.deltaTime;

            if (gatherMaterialTimer >= currentRequiredGatherTimer)
            {
                onGatherMaterial.Raise();
                gatherMaterialTimer = 0;
            }
        }
        else
        {
            gatherMaterialTimer = 0f;

        }

    }

    void Movement()
    {
        // Get input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        bodyAnimator.SetFloat("x", horizontalInput);
        bodyAnimator.SetFloat("y", verticalInput);

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        transform.position += movement * moveSpeed * Time.deltaTime;
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

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused)
            {
                onPauseMenuShow.Raise(true);
                gamePaused = true;
            }
            else
            {
                onPauseMenuShow.Raise(false);
                gamePaused = false;
            }

        }
    }
}
