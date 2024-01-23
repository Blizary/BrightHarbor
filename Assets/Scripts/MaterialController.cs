using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] private float gatherTimer;
    [SerializeField] private GameEventListener onGatherMaterial;
    [SerializeField] private GameEvent_Float onGatherTimerUpdate;

    private bool playerInRage = false;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        onGatherMaterial.Response.AddListener(OnGatherMaterial);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGatherMaterial()
    {
        Destroy(gameObject);
        //animator.SetTrigger("Consumed");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            if(!playerInRage)
            {
                onGatherTimerUpdate.Raise(gatherTimer);
            }
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
