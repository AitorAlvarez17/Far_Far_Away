using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceBugController : MonoBehaviour
{
    [SerializeField] GameEvent spaceBugKill;
    [SerializeField] GameManager gameManager;
    [SerializeField] private bool isAttacking = false;

    public void ChangeAnimation()
    {
        if (isAttacking) GetComponentInChildren<Animator>().Play("Attack");
    }

    public void OnHit()
    {
        spaceBugKill.Raise();

        this.gameObject.SetActive(false);
    }
}
