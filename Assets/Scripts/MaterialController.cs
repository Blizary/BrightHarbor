using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MaterialType
{
    LIGHTFRAGMENT,
    WOOD,
    STONE,
    METAL,
    GEM
}
public class MaterialController : MonoBehaviour
{
    [SerializeField] private MaterialType type;
    [SerializeField] private float amount;
    [SerializeField] private float gatherTimer;
    [SerializeField] private GameEventListener onGatherMaterial;
    [SerializeField] private GameEvent_Float onGatherTimerUpdate;

    [SerializeField] private bool playerInRage = false;
    private DataManager dataManager;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        onGatherMaterial.Response.AddListener(OnGatherMaterial);
        dataManager = GameObject.FindFirstObjectByType<DataManager>();
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
        if(playerInRage)
        {
            MaterialAmount newMaterialAmount = new MaterialAmount();
            newMaterialAmount.materialType = type;
            newMaterialAmount.amount = amount;
            dataManager.AddMaterials(newMaterialAmount);
            //animator.SetTrigger("Consumed");
            Destroy(gameObject);
        }
       

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
