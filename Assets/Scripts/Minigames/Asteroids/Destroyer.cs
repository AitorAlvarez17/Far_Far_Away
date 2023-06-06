using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
   
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "AStdestroyer" )
        {
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Ship")
        {
            //play a sound like an explosion
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
   
}
