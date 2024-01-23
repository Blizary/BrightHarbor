using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener_Bool : MonoBehaviour
{
    public GameEvent_Bool Event;
    public UnityEvent_Bool Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(bool s)
    {
        Response.Invoke(s);
    }
}


[System.Serializable]
public class UnityEvent_Bool : UnityEvent<bool>
{
}
