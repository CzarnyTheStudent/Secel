using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityGameEventLestener : MonoBehaviour, IGameEventListener
{
    public GameEvent gameEvents;
    public UnityEvent response;


    public void OnEventRaise()
    {
        response.Invoke();
    }

    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        response
    }
}
