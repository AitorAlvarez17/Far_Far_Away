using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTitle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float length;

    private float initialHeight;

    private void Start()
    {
        initialHeight = transform.position.y;
    }

    private void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, length) + initialHeight;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
