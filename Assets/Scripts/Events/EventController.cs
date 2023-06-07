using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    [SerializeField] public List<GameEvent> events;
    [SerializeField] public GameEvent endEvent;

    public void Start()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i].called = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetRandomEvent();
        }
    }

    public GameEvent GetRandomEvent()
    {
        if (GameFinished())
        {
            EndEvent();
            return null;
        }

        int randomIndex = Random.Range(0, events.Count);
        GameEvent randomEvent = events[randomIndex];
        if (!randomEvent.called)
        {
            randomEvent.called = true;
            Debug.Log(randomEvent.name);
            StartGameEvent(randomEvent);
            return randomEvent;
        }
        else return GetRandomEvent();
    }

    private bool GameFinished()
    {
        int completedEvents = 0;

        for (int i = 0; i < events.Count; i++)
        {
            if (events[i].called)
                completedEvents++;
        }

        return completedEvents == events.Count;
    }

    public void StartGameEvent(GameEvent randomEvent)
    {
        randomEvent.Raise();
    }

    public void EndEvent()
    {
        endEvent.Raise();
    }
}