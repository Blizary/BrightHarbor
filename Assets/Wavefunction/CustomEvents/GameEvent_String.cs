using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_String", menuName = "Wavefunction/Event/New GameEvent_String", order = 1)]
public class GameEvent_String : ScriptableObject
{
    private List<GameEventListener_String> listeners = new List<GameEventListener_String>();

    public void Raise(string s)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(s);
        }
    }

    public void RegisterListener(GameEventListener_String listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener_String listener)
    {
        listeners.Remove(listener);
    }
}
