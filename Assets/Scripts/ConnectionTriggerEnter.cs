using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionTriggerEnter : MonoBehaviour
{
    [SerializeField] private GameEventListener_Vector3 onEnterRoom;
    [SerializeField] private GameEvent onEnterConnection;

    [SerializeField] bool hasBeenTriggered = false;

    private void Awake()
    {
        onEnterRoom.Response.AddListener(OnEnterRoom);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenTriggered)
        {
            if (collision.CompareTag("PlayerBody"))
            {
                onEnterConnection.Raise();
                hasBeenTriggered = true;
            }
        }

    }

    void OnEnterRoom(Vector3 _pos)
    {
        hasBeenTriggered = false;
    }
}
