using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventDisplay : MonoBehaviour
{
    public EventController controller;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //public void ResetText()
    //{
    //    controller.Randomize();
    //    Debug.Log("Reseting Text");
    //    for(int i = 0; i < controller.scenes.Count; i++)
    //    {
            
    //        text.text += controller.scenes[i].name +". ";
         
    //    }
    //    Debug.Log(text.text);
    //}
}
