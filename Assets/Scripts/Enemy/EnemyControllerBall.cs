using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBall : EnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        
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

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!dead)
        {

            if (other.CompareTag("PlayerBody"))
            {
                onPlayerHit.Raise(damageOnHit);
            }
        }

    }
}
