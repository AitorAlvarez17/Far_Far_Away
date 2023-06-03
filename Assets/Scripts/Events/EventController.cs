using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EventController : MonoBehaviour
{
    [SerializeField] public List<GameEvent> events;

    public void Start()
    {
        Randomize();
    }
    public void Randomize()
    {
        Debug.Log("Randomization Start!");

        for (int i = 0; i < events.Count; i++)
        {
            GameEvent temp = events[i];
            int randomIndex = Random.Range(i, events.Count);
            events[i] = events[randomIndex];
            events[randomIndex] = temp;

            StartGameEvent(temp);
        }
    }

    public void StartGameEvent(GameEvent ev)
    {
        ev.Raise();
    }
}