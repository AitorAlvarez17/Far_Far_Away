using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Awake : MonoBehaviour
{
    public InfimaGames.LowPolyShooterPack.Weapon gun;

    // Start is called before the first frame update
    void Start()
    {
        gun.AwakeGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
