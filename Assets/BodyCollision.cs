using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCollision : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform head;
    public Transform feet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(head.position.x, feet.position.y, head.position.z);
    }
}
