using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Add GameEventListener class to object for event listening
public class GameEventListener : MonoBehaviour 
{
    public GameEvent gameEvent; // gameEvent to listen to
    public UnityEvent response; // response of gameEvent
    //int conveyorBelt_status;

    private void OnEnable()
    {
        gameEvent.RegisterListener(this); // add listener to list/array of listeners to respective gameEvent
    }
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this); // unregister
    }

    public void OnEventRaised()
    {
        response.Invoke(); // invoke response 
    }
}
