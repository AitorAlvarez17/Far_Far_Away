using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator animatorController;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 asteroidsPosition;
    [SerializeField] private Quaternion asteroidsRotation;
    [SerializeField] private Vector3 azoteaPosition;
    [SerializeField] private Quaternion azoteaRotation;

    private bool isFlashEvent = false;

    public void Arrives()
    {
        isFlashEvent = true;

        animatorController.Play("Idle");
        transform.position = startPosition;
    }

    public void Leaves()
    {
        this.gameObject.SetActive(false);
    }

    public void GoesAzotea()
    {
        animatorController.Play("Scared");

        transform.position = azoteaPosition;
        transform.rotation = azoteaRotation;
    }

    public void Asteroids()
    {
        if (!gameManager.withFlash) return;
        
        animatorController.Play("Scared");

        transform.position = asteroidsPosition;
        transform.rotation = asteroidsRotation;
    }

    public void FaultyShip()
    {
        if (!gameManager.withFlash) return;

        animatorController.Play("Scared");

        transform.position = startPosition;

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
