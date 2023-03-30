using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class firing : MonoBehaviour
{
    public InfimaGames.LowPolyShooterPack.Weapon gun;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Axis1D.SecondaryIndexTrigger"))
        {
            gun.Fire();
            
        }
    }
}
