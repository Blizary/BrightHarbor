using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour
{
    [SerializeField] private GameEvent_Bool onExitMenuShow;

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBody"))
        {
            Debug.Log("stop! exit time");
            onExitMenuShow.Raise(true);
        }

    }
}
