using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener_Vector3 : MonoBehaviour
{
    public GameEvent_Vector3 Event;
    public UnityEvent_Vector3 Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(Vector3 s)
    {
        Response.Invoke(s);
    }
}


[System.Serializable]
public class UnityEvent_Vector3 : UnityEvent<Vector3>
{
}