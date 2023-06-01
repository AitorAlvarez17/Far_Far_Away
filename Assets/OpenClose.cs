using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClose : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public bool isOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnButtonSwitch()
    {
        isOpen = !isOpen;
    }

    // Update is called once per frame
    void Update()
    {

        if (isOpen == true)
        {
            animator.Play("Open");
        }
        else if(isOpen == false)
        {
            animator.Play("Closing");
        }
    }
}
