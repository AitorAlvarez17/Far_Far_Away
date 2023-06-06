using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private int cooldown;
    [SerializeField] private bool isLight = false;
    public bool isCrazy = false;
    private int counter = 0;
 
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
        if (!isLight) isOpen = false;
        else this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isOpen && !isLight)
        {
            animator.Play("Open");
        }
        else if (!isOpen && !isLight)
        {
            animator.Play("Close");
        }
        
        if (isCrazy)
        {
            counter++;
            if (counter == cooldown)
            {
                if (!isLight)
                {
                    if (isOpen) isOpen = false;
                    else isOpen = true;
                }
                else
                {
                    Light light = this.GetComponent<Light>();
                    light.enabled = !light.enabled;
                }

                counter = 0;
            }
        }
    }
}
