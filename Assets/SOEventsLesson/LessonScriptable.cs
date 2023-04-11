using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SuperGiera/NewGameIvents", fileName = "NewGameEvent")]
public class LessonScriptable : MonoBehaviour
{
    public List<IGameEventListener> gameEventListener = new List<IGameEventListener>();


    public void Raise()
    {
        for (int i = 0; i < gameEventListener.Count; i++)
        {
            gameEventListener[i].OnEventRaise();
        }
    }

    public void RegisterListener(IGameEventListener gameEventListeners)
    {
        if(gameEventListener.Contains(gameEventListeners))
            return;
        gameEventListener.Add(gameEventListeners);
    }

    public void UnregisterListener(IGameEventListener gameEventListeners)
    {
        if (gameEventListener.Contains(gameEventListeners))
            return;
        gameEventListener.Remove(gameEventListeners);
    }




}
