using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidController : MonoBehaviour
{
    public bool isActive = false;
    public List<GameObject> objectPrefabs; // Reference to the object prefab
    public GameObject asteroidParent;
    public Vector3 spawnDirection; // Direction in which the objects will move

    public float spawnInterval = 1f; // Time interval between object spawns
    public float objectSpeed = 5f; // Speed of the spawned objects
    public float objectAngularSpeed = 2.5f;

    private Coroutine spawnCoroutine; // Reference to the coroutine
    public float shipSpeed = 5.0f;
    Vector2 joystickVec;

    private void Start()
    {
        // Start spawning objects at the specified interval
    }

    private void OnDestroy()
    {
        // Stop spawning objects when the script or GameObject is destroyed
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }
    void Update()
    {
        //SpawnObjects();

        if(isActive)
        UpdateParentPosition(shipSpeed);
    }

    public void StartEvent()
    {
        spawnCoroutine = StartCoroutine(SpawnObjects());
        isActive = true;
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            
            // Generate a random position on the plane
            Vector3 spawnPosition = new Vector3(150f, UnityEngine.Random.Range(-100f, 100f), UnityEngine.Random.Range(-100f, 100f));
            int index = 0;

            if (objectPrefabs.Count == 0)
            {
                Debug.LogWarning("The gameObjectsList is empty.");
                break;
            }
            else
            {
                index = UnityEngine.Random.Range(0, objectPrefabs.Count);
            }
            // Instantiate the object prefab at the random position
            GameObject spawnedObject = Instantiate(objectPrefabs[index], spawnPosition, Quaternion.identity, asteroidParent.transform);
            spawnedObject.transform.localScale *= UnityEngine.Random.Range(1.5f, 3f);
            spawnedObject.transform.rotation = UnityEngine.Random.rotation;
            Vector3 velocity = spawnDirection.normalized * objectSpeed;

            // Set the initial velocity of the spawned object based on the spawn direction and speed
            Rigidbody objectRigidbody = spawnedObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                objectRigidbody.velocity = velocity;
                objectRigidbody.angularVelocity = UnityEngine.Random.insideUnitSphere * objectAngularSpeed;
            }

         
            yield return new WaitForSeconds(spawnInterval);
            
            
        }
    }

    void UpdateParentPosition(float speed)
    {
        var parentPos = asteroidParent.transform;

        parentPos.position += new Vector3(0f, joystickVec.x * speed * Time.deltaTime, joystickVec.y * speed * Time.deltaTime);
    }

    public void JoystickVChangeX(float x)
    {
        joystickVec.x = -x;
    }

    /// <summary>
    /// Gets the Y value of the joystick. Called by the <c>XRJoystick.OnValueChangeY</c> event.
    /// </summary>
    /// <param name="y">The joystick's Y value</param>
    public void JoystickChangeY(float y)
    {
        joystickVec.y = -y;
    }
}
