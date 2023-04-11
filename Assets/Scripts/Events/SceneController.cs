using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    public List<SceneAsset> scenes;

    // Start is called before the first frame update
    void Start()
    {
        foreach (SceneAsset sc in scenes)
            Debug.Log("'" + sc.name + "'");

        Randomize();

    }

    public void Randomize()
    {
        Debug.Log("Randomization Start!");

        for (int i = 0; i < scenes.Count; i++)
        {
            SceneAsset temp = scenes[i];
            int randomIndex = Random.Range(i, scenes.Count);
            scenes[i] = scenes[randomIndex];
            scenes[randomIndex] = temp;
        }

        foreach (SceneAsset sc in scenes)
            Debug.Log("'" + sc.name + "'");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
