using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_Integer", menuName = "Wavefunction/Event/New GameEvent Integer", order = 1)]
public class GameEvent_Integer : ScriptableObject
{
    private List<GameEventListener_Integer> listeners = new List<GameEventListener_Integer>();

    public void Raise(int s)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(s);
        }
    }

    public void RegisterListener(GameEventListener_Integer listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener_Integer listener)
    {
        listeners.Remove(listener);
    }
}
