using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerController : MonoBehaviour
{
    [SerializeField] private RoomController roomController;
    [SerializeField] private GameEvent_Vector3 onEnterRoom;
    [SerializeField] private GameEventListener onEnterConnection;

    [SerializeField] bool hasBeenTriggered = false;

    private void Awake()
    {
        onEnterConnection.Response.AddListener(OnEnterConnection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!hasBeenTriggered)
        {
            if (collision.CompareTag("PlayerBody"))
            {
                onEnterRoom.Raise(roomController.GetRoomCenter());
                roomController.OnEnterRoom();
                hasBeenTriggered = true;
            }
        }
        
    }

    void OnEnterConnection()
    {
        hasBeenTriggered = false;
        roomController.LeftRoom();
    }

}
