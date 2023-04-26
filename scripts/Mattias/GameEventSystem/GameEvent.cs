using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>(); // list of listeners

    public void Raise() // on raise iterate through listeners for invoking response / raising event
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener) // if current list of listeners does not contain listener -> add
    {
        if (!listeners.Contains(listener))
        {
            listeners.Add(listener);
        }
    }
    public void UnregisterListener(GameEventListener listener) // un register listener
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }

    }
}
