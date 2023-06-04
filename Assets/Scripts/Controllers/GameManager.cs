using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameEvent gameOverGameEvent;

    [HideInInspector] public bool withFlash = false;
    private int shipHealth = 3;

    public int GetHealth() { return shipHealth; }
    public void LoseHealth() 
    { 
        shipHealth--;
        if (shipHealth == 0)
        {
            gameOverGameEvent.Raise();
        }
    }

    public void FlashStays()
    {
        withFlash = true;
    }

    public void FlashMinigameStart()
    {

    }

    public void FlashMinigameEnd()
    {

    }
}
