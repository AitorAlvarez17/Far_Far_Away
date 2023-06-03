using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private int cooldown;
    public bool isCrazy = false;
    private int counter = 0;
 
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnButtonSwitch()
    {
        if (!isCrazy)
            isOpen = !isOpen;
    }

    public void GoesCrazy()
    {
        isCrazy = true;
    }

    public void CalmsDown()
    {
        isCrazy = false;
    }

    void Update()
    {
        if (isOpen)
        {
            animator.Play("Open");
        }
        else if (!isOpen)
        {
            animator.Play("Close");
        }
        
        if (isCrazy)
        {

            counter++;
            if (counter == cooldown)
            {
                if (isOpen) isOpen = false;
                else isOpen = true;

                counter = 0;
            }
        }
    }
}
