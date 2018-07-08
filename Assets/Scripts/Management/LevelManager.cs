using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EVERY Level must have a LevelManager
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Placeables;

    private GameObject[] players = new GameObject[2];

    void Start()
    {
//        for (int i = 1; i < 3; i++)
//        {
//            GameObject player = GameObject.Find("/P" + i);
//            players[i - 1] = player;
//            List<GameObject> ghostInstances = new List<GameObject>();
//            GameObject ghost;
//            PlaceObjects playerPOScript = player.GetComponent<PlaceObjects>();
//            foreach (GameObject g in Placeables)
//            {
//                ghost = Instantiate(g, player.transform);
//                ghost.transform.localPosition = Vector2.left * playerPOScript.ghostDistance;
//                ghost.SetActive(false);
//                ghostInstances.Add(ghost);
//            }
//            playerPOScript.SetGhostList(ghostInstances);
//        }
    }

    void Update()
    {
        
    }
}