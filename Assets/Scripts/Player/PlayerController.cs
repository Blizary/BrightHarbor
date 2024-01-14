using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameEvent onAttackTrigger;
    private bool mousePressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(!mousePressed)
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
}
