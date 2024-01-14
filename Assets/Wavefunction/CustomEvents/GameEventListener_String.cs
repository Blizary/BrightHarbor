using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener_String : MonoBehaviour
{
    public GameEvent_String Event;
    public UnityEvent_String Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(string s)
    {
        Response.Invoke(s);
    }
}


[System.Serializable]
public class UnityEvent_String : UnityEvent<string>
{
}
