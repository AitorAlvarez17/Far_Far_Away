using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject gameObj;
    public GameManager controller;
    // Start is called before the first frame update


    private void Start()
    {
        gameObj = GameObject.Find("GameManager");
        controller = gameObj.GetComponent<GameManager>();
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
            //play a sound like an explosion
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
   
}
