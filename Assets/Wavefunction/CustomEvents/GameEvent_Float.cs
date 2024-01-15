using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_Float", menuName = "Wavefunction/Event/New GameEvent Float", order = 1)]
public class GameEvent_Float : ScriptableObject
{
    private List<GameEventListener_Float> listeners = new List<GameEventListener_Float>();

    public void Raise(float s)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(s);
        }
    }

    public void RegisterListener(GameEventListener_Float listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener_Float listener)
    {
        listeners.Remove(listener);
    }
}
