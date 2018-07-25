using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public Text DisplayText;
    public Image DisplayImage;
    public Queue<string> texts = new Queue<string>();
    public Queue<Sprite> images = new Queue<Sprite>();
    public Animator anim;
    public bool is_over;
    private bool up;
    // Use this for initialization
    private void Start()
    {
        is_over = false;
    }
    private void Update()
    {
        if (Input.GetButtonUp("A_P1") == true || Input.GetButtonUp("A_P2") == true)
        {
            DisplayNextSentence();
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("Trigger", true);
        texts.Clear();
        images.Clear();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<PlaceObjects>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
        }
        foreach (string text in dialogue.sentences)
        {
            texts.Enqueue(text);
        }
        foreach (Sprite image in dialogue.images)
        {
            images.Enqueue(image);
        }
        DisplayNextSentence();

    }
    public void DisplayNextSentence()
    {
        if (texts.Count == 0)
        {
            EndConversation();
            return;
        }
        string text = texts.Dequeue();
        DisplayImage.sprite = images.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(text));
    }
    IEnumerator TypeSentence(string sentence)
    {
        DisplayText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DisplayText.text += letter;
            yield return null;
        }
    }
    public void EndConversation()
    {

        anim.SetBool("Trigger", false);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<PlaceObjects>().enabled = true;
            player.GetComponent<PlayerMovement>().enabled = true;
        }
        is_over = true;
        GameObject[] dialogues = GameObject.FindGameObjectsWithTag("Dialogue");

    }
}
