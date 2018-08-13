using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusSwitch : MonoBehaviour
{
    private List<GameObject> beams = new List<GameObject>();
    private TextMesh displayText;
    public int threshold;
    protected Light lightsource;
    [SerializeField]
    protected List<GameObject> ControlledItems;
    private void Start()
    {
        lightsource = transform.GetChild(0).GetComponent<Light>();
        displayText = transform.GetChild(1).GetComponent<TextMesh>();
    }
    private void LateUpdate()
    {
        int NumOfHits = beams.Count;
        lightsource.intensity = 5 * NumOfHits;
        bool isActive = NumOfHits >= threshold ? true : false;
        foreach (var item in ControlledItems)
        {
            // NOTE: create and use Interactable interface instead
            // item.GetComponent<Interactable>().activate();
            var door = item.GetComponent<Door>();
            if (door != null) door.SetDoorState(isActive);
        }
        displayText.text = (threshold - NumOfHits).ToString();
        beams.Clear();
    }
    public void Activate(GameObject beam)
    {
        if (!beams.Contains(beam))
        {
            beams.Add(beam);
        }
    }
}
