using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public Dialogue dialogue;
    public int sequence;

    public void triggerDialogue()
    {
        FindObjectOfType<DialogManager>().StartDialogue(dialogue);

    }
}
