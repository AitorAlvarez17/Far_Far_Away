using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EventController eventController;
    [SerializeField] GameEvent gameOverGameEvent;
    [SerializeField] private bool tutorialCompleted = false;
    [SerializeField] private bool eventInCourse = false;
    [SerializeField] private float cooldownForEvent;
    
    [Header("Flash Minigame")]
    [SerializeField] public int enemyCounter = 0;

    /*[HideInInspector] */public bool withFlash = false;
    private int shipHealth = 3;
    private float timer = 0;

    private void Start()
    {
        tutorialCompleted = false;
        eventInCourse = false;
        timer = 0;
    }

    private void Update()
    {
        if (!tutorialCompleted)
            return;

        if (eventInCourse)
            return;

        timer += Time.deltaTime;

        if (timer > cooldownForEvent)
        {
            eventController.GetRandomEvent();
            eventInCourse = true;

            timer = 0;
        }
    }
    public int GetHealth() { return shipHealth; }
    public void LoseHealth() 
    { 
        shipHealth--;
        if (shipHealth == 0)
        {
            gameOverGameEvent.Raise();
        }
    }

    public void TutorialCompleted()
    {
        tutorialCompleted = true;
    }

    public void FlashStays()
    {
        withFlash = true;
    }

    public void FlashMinigameStart()
    {

    }

    public void OnSpaceBugKill()
    {
        enemyCounter++;

        if (enemyCounter == 5)
        {
            Debug.Log("moltbe");
        }
    }

    public void EventCompleted()
    {
        eventInCourse = false;
    }
}
