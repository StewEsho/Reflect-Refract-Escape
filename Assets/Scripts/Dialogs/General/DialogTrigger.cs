using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public Dialogue dialogue;
    public int sequence;
    public bool is_over;

    private void Update()
    {
        is_over = FindObjectOfType<DialogueManager>().is_over;
    }
    public void triggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }
}
