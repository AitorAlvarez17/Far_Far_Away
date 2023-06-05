using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidController : MonoBehaviour
{
    public List<GameObject> objectPrefabs; // Reference to the object prefab
    public Vector3 spawnDirection; // Direction in which the objects will move

    public float spawnInterval = 1f; // Time interval between object spawns
    public float objectSpeed = 5f; // Speed of the spawned objects

    private Coroutine spawnCoroutine; // Reference to the coroutine

    private void Start()
    {
        // Start spawning objects at the specified interval
        spawnCoroutine = StartCoroutine(SpawnObjects());
    }

    private void OnDestroy()
    {
        // Stop spawning objects when the script or GameObject is destroyed
        if (spawnCoroutine != null)
            StopCoroutine(spawnCoroutine);
    }
    void Update()
    {
        SpawnObjects();
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Generate a random position on the plane
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), 0f, UnityEngine.Random.Range(-10f, 10f));
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
            GameObject spawnedObject = Instantiate(objectPrefabs[index], spawnPosition, Quaternion.identity);

            // Set the initial velocity of the spawned object based on the spawn direction and speed
            Rigidbody objectRigidbody = spawnedObject.GetComponent<Rigidbody>();
            if (objectRigidbody != null)
            {
                objectRigidbody.velocity = spawnDirection.normalized * objectSpeed;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
