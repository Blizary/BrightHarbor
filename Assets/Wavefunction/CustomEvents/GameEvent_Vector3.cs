using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent_Vector3", menuName = "Wavefunction/Event/New GameEvent Vector3", order = 9)]
public class GameEvent_Vector3 : ScriptableObject
{
    private List<GameEventListener_Vector3> listeners = new List<GameEventListener_Vector3>();

    public void Raise(Vector3 s)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(s);
        }
    }

    public void RegisterListener(GameEventListener_Vector3 listener)
    {
        listeners.Add(listener);
    }

    public void UnRegisterListener(GameEventListener_Vector3 listener)
    {
        listeners.Remove(listener);
    }
}
