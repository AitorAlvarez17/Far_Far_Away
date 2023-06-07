using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject gameObj;
    public GameManager controller;
    AudioSource aSource;
    // Start is called before the first frame update


    private void Start()
    {
        gameObj = GameObject.Find("GameManager");
        controller = gameObj.GetComponent<GameManager>();
        aSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "AStdestroyer" )
        {
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Ship")
        {
            controller.AsteroidHit();
            aSource.PlayOneShot(aSource.clip);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
   
}
