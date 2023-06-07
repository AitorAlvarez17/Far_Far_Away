using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private int cooldown;
    [SerializeField] private bool isLight = false;
    [SerializeField] public AudioClip openClip;
    [SerializeField] public AudioClip closeClip;
    [SerializeField] public AudioSource aSource;
    public bool isCrazy = false;
    bool sound = false;
    private int counter = 0;
 
    public void OnButtonSwitch()
    {
        if (!isCrazy)
        {
            isOpen = !isOpen;
            sound = false;
        }
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
            if(!sound)
            {
                aSource.PlayOneShot(openClip);
                sound = true;
            }
        }
        else if (!isOpen && !isLight)
        {
            animator.Play("Close");
            if (!sound)
            {
                aSource.PlayOneShot(closeClip);
                sound = true;
            }

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
