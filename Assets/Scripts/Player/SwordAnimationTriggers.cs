using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimationTriggers : MonoBehaviour
{
    [SerializeField] private GameEvent onAttackFinnished;
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
}
