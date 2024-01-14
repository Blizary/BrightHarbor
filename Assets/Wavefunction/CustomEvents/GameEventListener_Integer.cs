using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener_Integer : MonoBehaviour
{
    public GameEvent_Integer Event;
    public UnityEvent_Integer Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnRegisterListener(this);
    }

    public void OnEventRaised(int s)
    {
        Response.Invoke(s);
    }
}


[System.Serializable]
public class UnityEvent_Integer : UnityEvent<int>
{
}
