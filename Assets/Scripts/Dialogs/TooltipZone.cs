using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TooltipZone : MonoBehaviour
{
	public GameObject ButtonPrompt;
	private GameObject promptP1, promptP2;
	private int numOfPlayersInside = 0;
	private Sprite icon;

	void Awake()
	{
		icon = Resources.Load<Sprite>("Xbox One/XboxOne_X");
		ButtonPrompt.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = icon;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			numOfPlayersInside++;
			if (other.transform.name == "P1") //duplicate code >:(
			{
				promptP1 = Instantiate(ButtonPrompt, other.transform); //perhaps use object pooling instead ;)
				promptP1.transform.localPosition = Vector2.up * 1.5f;
			}
			else if (other.transform.name == "P2") //duplicate code >:(
			{
				promptP2 = Instantiate(ButtonPrompt, other.transform); //perhaps use object pooling instead ;)
				promptP2.transform.localPosition = Vector2.up * 1.5f;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player") && numOfPlayersInside <= 1)
		{
			if (other.transform.name == "P1") //duplicate code >:(
			{
				Destroy(promptP1);
				promptP1 = null;
			}
			else if (other.transform.name == "P2") //duplicate code >:(
			{
				Destroy(promptP2);
				promptP2 = null;
			}

			numOfPlayersInside--;
		}
	}
}
