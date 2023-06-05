using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GameObject player;
    [SerializeField] private Vector3 pilotPosition;
    [SerializeField] private Quaternion pilotRotation;

    [Header("Dialogues")]
    [SerializeField] public NPCConversation TutorialConversation;
    [SerializeField] public NPCConversation TutorialConversation2;
    [SerializeField] public NPCConversation FlashConversation;
    [SerializeField] public NPCConversation FlashConversation2;
    [SerializeField] public NPCConversation FlashConversationMinigameWin;
    [SerializeField] public NPCConversation FlashConversationMinigameLose;

    [Header("Events")]
    [SerializeField] EventController eventController;
    [SerializeField] GameEvent gameOverGameEvent;
    [SerializeField] GameEvent flashLeavesGameEvent;
    [SerializeField] GameEvent flashMinigameEndGameEvent;
    [SerializeField] private bool tutorialCompleted = false;
    [SerializeField] private bool eventInCourse = false;
    [SerializeField] private float cooldownForEvent;
    
    [Header("Flash Minigame")]
    [SerializeField] public int enemyCounter = 0;
    [SerializeField] public float timeToKillBugs = 10f;
    private bool isFlashMinigame = false;
    private float bugTimer = 0f;

    [Header("Asteroids Minigame")] // No se
    private bool isAsteroidsMinigame = false;
    [SerializeField] public float speed = 0f;

    [Header("Ship Malfunction Minigame")]
    private bool isShipMalfunctionMinigame = false;
    [SerializeField] public float timeToFindBatteries = 30f;
    private float batteriesTimer = 0f;

    [HideInInspector] public bool withFlash = false;
    private int shipHealth = 3;
    private float timer = 0;

    private void Start()
    {
        tutorialCompleted = false;
        eventInCourse = false;
        timer = 0;

        RepositionPlayer();

        // Tutorial comienza, lanzando primer dialogo
        ConversationManager.Instance.StartConversation(TutorialConversation);
    }

    private void RepositionPlayer()
    {
        // Posicionando al jugador en el asiento piloto
        player.transform.position = pilotPosition;
        player.transform.rotation = pilotRotation;

        // Hay que quitarle el control para que no se vaya
    }

    public void NextTutorialConversation()
    {
        ConversationManager.Instance.StartConversation(TutorialConversation2);
    }

    private void Update()
    {
        if (!tutorialCompleted)
            return;

        /// FLASH ///
        if (isFlashMinigame)
        {
            bugTimer += Time.deltaTime;

            if (bugTimer > timeToKillBugs)
            {
                LoseFlashMinigame();
            }
        }

        /// ASTEROIDS ///
        if (isAsteroidsMinigame)
        {
            
        }

        /// SHIP MALFUNCTION ///
        if (isShipMalfunctionMinigame)
        {
            batteriesTimer += Time.deltaTime;

            if (batteriesTimer > timeToFindBatteries)
            {
                // Lose minigame
            }
        }
        /// GENERAL ///
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

    /// GENERAL ///
    public int GetHealth() { return shipHealth; }
    public void LoseHealth() 
    { 
        shipHealth--;
        if (shipHealth == 0)
        {
            gameOverGameEvent.Raise();
        }
    }

    public void EventCompleted()
    {
        eventInCourse = false;
    }

    private void LoseMinigame()
    {
        EventCompleted();
        LoseHealth();
    }

    /// TUTORIAL ///
    public void TutorialCompleted()
    {
        tutorialCompleted = true;
    }

    /// FLASH ///

    public void FlashStays()
    {
        withFlash = true;
    }

    public void FlashArrives()
    {
        ConversationManager.Instance.StartConversation(FlashConversation);
    }
    public void FlashLeaves()
    {
        withFlash = false;
        flashLeavesGameEvent.Raise();
    }

    public void NextFlashConversation()
    {
        ConversationManager.Instance.StartConversation(FlashConversation2);
    }
    public void OnSpaceBugKill()
    {
        isFlashMinigame = true;

        enemyCounter++;

        if (enemyCounter == 5) WinFlashMinigame();
    }

    private void WinFlashMinigame()
    {
        ConversationManager.Instance.StartConversation(FlashConversationMinigameWin);
        flashMinigameEndGameEvent.Raise();
        EventCompleted();
    }
    
    private void LoseFlashMinigame()
    {
        ConversationManager.Instance.StartConversation(FlashConversationMinigameLose);
        flashMinigameEndGameEvent.Raise();
        LoseMinigame();
    }


    /// ASTEROIDS ///


    /// SHIP MALFUNCTION ///

}
