using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener_Float : MonoBehaviour
{
    public GameEvent_Float Event;
    public UnityEvent_Float Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(float s)
    {
        Response.Invoke(s);
    }
}


[System.Serializable]
public class UnityEvent_Float : UnityEvent<float>
{
}
