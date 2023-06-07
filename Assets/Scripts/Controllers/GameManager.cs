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
    [SerializeField] public NPCConversation ShipMalfunctionConversation1;
    [SerializeField] public NPCConversation ShipConversationMinigameWin;
    [SerializeField] public NPCConversation ShipConversationMinigameLose;
    [SerializeField] public NPCConversation AsteroidsConversation;
    [SerializeField] public NPCConversation AsteroidsConversationMinigameWin;
    [SerializeField] public NPCConversation AsteroidsConversationMinigameLose;
    [SerializeField] public NPCConversation EndConversation;

    [Header("Events")]
    [SerializeField] EventController eventController;
    [SerializeField] GameEvent gameOverGameEvent;
    [SerializeField] GameEvent flashLeavesGameEvent;
    [SerializeField] GameEvent flashMinigameEndGameEvent;
    [SerializeField] GameEvent shipMinigameEndGameEvent;
    [SerializeField] GameEvent AsteroidsEndGameEvent;
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
    [SerializeField] public float timeToCompleteAsteroids = 150f;
    private float asteroidTimer = 0f;
    public int asteroidHealth = 100;

    [SerializeField] public float speed = 0f;

    [Header("Ship Malfunction Minigame")]
    [SerializeField] private List<BatterySlotController> batterySlots = new List<BatterySlotController>();
    private bool isShipMalfunctionMinigame = false;
    [SerializeField] public float timeToFindBatteries = 120f;
    private float batteriesTimer = 0f;
    private int batteryCount = 0;

    [HideInInspector] public bool withFlash = false;
    private int shipHealth = 3;
    private float timer = 0;
    
    [Header("Particles")]
    [SerializeField] public ParticleSystem hyperspace;
    // add explosion particles

    private void Start()
    {
        //tutorialCompleted = false;
        eventInCourse = false;
        timer = 0;
        hyperspace.Stop();


        if (!tutorialCompleted)
        {
            RepositionPlayer();
            ConversationManager.Instance.StartConversation(TutorialConversation);
        }
    }

    private void RepositionPlayer()
    {
        // Posicionando al jugador en el asiento piloto
        player.transform.position = pilotPosition;
        player.transform.rotation = pilotRotation;

        // Hay que quitarle el control para que no se vaya
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
            asteroidTimer += Time.deltaTime;

            if(asteroidTimer >= timeToCompleteAsteroids && asteroidHealth >0)
            {
                AsteroidsWin();
            }
            else if(asteroidTimer < timeToCompleteAsteroids && asteroidHealth <= 0)
            {
                AsteroidsLose();
            }
        }

        /// SHIP MALFUNCTION ///
        if (isShipMalfunctionMinigame)
        {
            batteriesTimer += Time.deltaTime;

            if (batteriesTimer > timeToFindBatteries)
            {
                ShipMalfunctionLose();
            }

            if (batteryCount >= 3)
            {
                for (int i = 0; i < batterySlots.Count; i++)
                {
                    if (!batterySlots[i].hasBattery)
                        return;
                }

                ShipMalfunctionWin();
            }
        }
        /// GENERAL ///
        if (eventInCourse)
            return;

        timer += Time.deltaTime;

        if (timer > cooldownForEvent)
        {
            hyperspace.Stop();
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

    public void EndGame()
    {
        ConversationManager.Instance.StartConversation(EndConversation);
    }

    /// TUTORIAL ///
    public void TutorialCompleted()
    {
        tutorialCompleted = true;
    }

    public void NextTutorialConversation()
    {
        StartCoroutine(StartNextDialogue(TutorialConversation2, 4f));
    }

    /// FLASH ///

    public void FlashStays()
    {
        withFlash = true;

        // Setting parameters for dialogue
        //ConversationManager.Instance.SetBool("hasFlash", true);
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
        StartCoroutine(StartNextDialogue(FlashConversation2, 10f));
    }

    private IEnumerator StartNextDialogue(NPCConversation nextDialogue, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        ConversationManager.Instance.EndConversation();
        
        yield return new WaitForSeconds(2f);

        ConversationManager.Instance.StartConversation(nextDialogue);
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
        ConversationManager.Instance.SetInt("shipHealth", shipHealth);

        flashMinigameEndGameEvent.Raise();
        LoseMinigame();
    }


    /// ASTEROIDS ///

    public void AsteroidsStart()
    {
        isAsteroidsMinigame = true;

        ConversationManager.Instance.StartConversation(AsteroidsConversation);
        
    }

    /// SHIP MALFUNCTION ///
    public void ShipMalfunctionStart()
    {
        isShipMalfunctionMinigame = true;

        ConversationManager.Instance.StartConversation(ShipMalfunctionConversation1);
        ConversationManager.Instance.SetInt("shipHealth", shipHealth);
        ConversationManager.Instance.SetBool("hasFlash", withFlash);
    }

    public void AddBattery()
    {
        batteryCount++;
    }

    private void ShipMalfunctionWin()
    {
        isShipMalfunctionMinigame = false;
        ConversationManager.Instance.StartConversation(ShipConversationMinigameWin);
        ConversationManager.Instance.SetBool("hasFlash", withFlash);
        shipMinigameEndGameEvent.Raise();
        EventCompleted();
    }
    private void ShipMalfunctionLose()
    {
        isShipMalfunctionMinigame = false;
        ConversationManager.Instance.StartConversation(ShipConversationMinigameLose);
        ConversationManager.Instance.SetInt("shipHealth", shipHealth);
        ConversationManager.Instance.SetBool("hasFlash", withFlash);
        shipMinigameEndGameEvent.Raise();
        LoseMinigame();
    }

    public void AsteroidHit()
    {
        shipHealth -= 20;
    }

    private void AsteroidsWin()
    {
        isAsteroidsMinigame = false;
        ConversationManager.Instance.StartConversation(AsteroidsConversationMinigameWin);
        AsteroidsEndGameEvent.Raise();
        EventCompleted();
    }
    private void AsteroidsLose()
    {
        isAsteroidsMinigame = false;
        ConversationManager.Instance.StartConversation(AsteroidsConversationMinigameLose);
        AsteroidsEndGameEvent.Raise();
        LoseMinigame();
    }
}
