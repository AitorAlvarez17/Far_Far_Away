using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    [SerializeField] private Animator animatorController;

    public void Arrives()
    {
        animatorController.Play("Idle");
    }

    public void Asteroids()
    {
        animatorController.Play("Scared");
    }

    public void FaultyShip()
    {
        animatorController.Play("Scared");
    }

    public void Talks()
    {
        animatorController.Play("Talking");
    }

    public void StopsTalking()
    {
        animatorController.Play("Idle");
    }
}
