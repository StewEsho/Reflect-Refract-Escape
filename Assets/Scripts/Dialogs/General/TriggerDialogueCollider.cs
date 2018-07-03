using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueCollider : MonoBehaviour {
    public GameObject object_to_instantiate;
    public Vector3 position_to_instantiate;
    // Use this for initialization
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] dialogues = GameObject.FindGameObjectsWithTag("Dialogue");
        int min_val = dialogues[0].GetComponent<DialogTrigger>().sequence;
        foreach (GameObject dialogue in dialogues)
        {
            if (dialogue.GetComponent<DialogTrigger>().sequence < min_val)
            {
                min_val = dialogue.GetComponent<DialogTrigger>().sequence;
            }
        }
        if (collision.gameObject.tag == "Player" && GetComponent<DialogTrigger>().sequence == min_val)
        {
            GetComponent<DialogTrigger>().triggerDialogue();
            if (this.gameObject.tag == "Dialogue")
            {
                Destroy(this.gameObject);
            }
            else
            {
                this.gameObject.GetComponent<DialogTrigger>().enabled = false;
            }
        }
        if (object_to_instantiate != null)
        {
            Instantiate(object_to_instantiate, position_to_instantiate, Quaternion.identity);
        }
    }

}
