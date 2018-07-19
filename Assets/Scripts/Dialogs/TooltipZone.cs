using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TooltipZone : MonoBehaviour
{
	public GameObject Tooltip;
	[Header("Button Settings")]
	[Tooltip("Button to be pressed to dismiss the prompt for the player.")]
	public string ButtonPrompt;
	[SerializeField]
	[Tooltip("Do not set this field for an automatic button icon to be displayed.")]
	private Sprite icon;
	public bool IsRepeatable = false; //if true, tooltip will reappear on reentry even if initially dismissed
	public bool ForP1 = true; //active when p1 enters?
	public bool ForP2 = true; //active when p2 enters?
	
	private GameObject promptP1, promptP2;
	private bool hasP1Activated, hasP2Activated = false;
	private int numOfPlayersInside = 0;

	void Awake()
	{
//		if (icon == null)
//			icon = Resources.Load<Sprite>("Xbox One/ButtonPrompt");
		Tooltip.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = icon;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player"))
		{
			numOfPlayersInside++;
			if (ForP1 && other.transform.name == "P1" && !hasP1Activated) //duplicate code >:(
			{
				promptP1 = Instantiate(Tooltip, other.transform); //perhaps use object pooling instead ;)
				promptP1.transform.localPosition = Vector2.up * 1.5f;
				promptP1.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = icon;
			}
			else if (ForP2 && other.transform.name == "P2" && !hasP2Activated) //duplicate code >:(
			{
				promptP2 = Instantiate(Tooltip, other.transform); //perhaps use object pooling instead ;)
				promptP2.transform.localPosition = Vector2.up * 1.5f;
				promptP1.transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = icon;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		//holy shit the duplicate code this is probably the shittiest code i've ever written ~ stew
		//TODO: tell stew to be a better fuckin programmer this is a disgrace. stop being lazy and use a size 2 tuple
		if (ForP1 && Input.GetButtonDown(ButtonPrompt + "P1") && other.transform.name == "P1")
		{
			Destroy(promptP1);
			promptP1 = null;
			hasP1Activated = !IsRepeatable;
		}
		if (ForP2 && Input.GetButtonDown(ButtonPrompt + "P2") && other.transform.name == "P2")
		{
			Destroy(promptP2);
			promptP2 = null;
			hasP2Activated = !IsRepeatable;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.CompareTag("Player") && numOfPlayersInside <= 1)
		{
			if (ForP1 && other.transform.name == "P1") //duplicate code >:(
			{
				Destroy(promptP1);
				promptP1 = null;
			}
			else if (ForP2 && other.transform.name == "P2") //duplicate code >:(
			{
				Destroy(promptP2);
				promptP2 = null;
			}

			numOfPlayersInside--;
		}
	}
}
