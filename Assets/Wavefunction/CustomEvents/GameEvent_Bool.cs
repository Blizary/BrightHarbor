using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_Bool", menuName = "Wavefunction/Event/New GameEvent Bool", order = 1)]
public class GameEvent_Bool : ScriptableObject
{
    private List<GameEventListener_Bool> listeners = new List<GameEventListener_Bool>();

    public void Raise(bool s)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(s);
        }
    }

    public void RegisterListener(GameEventListener_Bool listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener_Bool listener)
    {
        listeners.Remove(listener);
    }
}

