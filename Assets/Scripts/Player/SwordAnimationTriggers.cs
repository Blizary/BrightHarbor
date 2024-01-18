using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimationTriggers : MonoBehaviour
{
    [SerializeField] private GameEvent onAttackFinnished;
    [SerializeField] private GameEvent_Integer onEnemyHit;
    [SerializeField] private SwordController swordController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAttackFinnished()
    {
        onAttackFinnished.Raise();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            onEnemyHit.Raise(swordController.GetDamage());
        }
    }
}
