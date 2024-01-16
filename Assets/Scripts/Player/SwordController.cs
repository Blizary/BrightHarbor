using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    [SerializeField] private GameEventListener_Integer onAttackTrigger;

    [SerializeField] private Animator animator;

    private void Awake()
    {
        onAttackTrigger.Response.AddListener(OnAttackTrigger);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnAttackTrigger(int _dir)
    {
        animator.SetTrigger("Attack");
    }
}
