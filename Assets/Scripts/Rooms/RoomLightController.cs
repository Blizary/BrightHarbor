using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLightController : MonoBehaviour
{
    [SerializeField] private GameEventListener onStealLight;
    [SerializeField] private GameEvent_Float onEffectLight;
    [SerializeField] private float closeToPlayerRadius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lightAmountOnConsumed;

    private bool movingToPlayer = false;
    [SerializeField] private bool playerInRage = false;
    private GameObject player;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
        onStealLight.Response.AddListener(OnStealLight);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (movingToPlayer)
        {
            // Calculate the direction to the target
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            // Move towards the target
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
            Debug.Log(distanceToTarget);
            if(distanceToTarget <= closeToPlayerRadius)
            {
                onEffectLight.Raise(lightAmountOnConsumed);
                gameObject.SetActive(false);
            }
        }
    }

    void OnStealLight()
    {
        if(!movingToPlayer)
        {
            if (playerInRage)
            {
                Debug.Log("Player in rage light being stolen");
                //move to player
                movingToPlayer = true;
                //trigger animation of turning off
                animator.SetTrigger("Consumed");
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBody"))
        {
            playerInRage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            playerInRage = false;
        }
    }
}
