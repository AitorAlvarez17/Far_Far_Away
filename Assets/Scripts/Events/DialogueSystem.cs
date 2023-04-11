using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueSystem : MonoBehaviour
{
    public void StartDialogue(NPCConversation dialogue)
    {
        ConversationManager.Instance.StartConversation(dialogue);
    }
}
