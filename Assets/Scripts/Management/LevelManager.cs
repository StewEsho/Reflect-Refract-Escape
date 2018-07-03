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
        for (int i = 1; i < 3; i++)
        {
            GameObject player = GameObject.Find("/P" + i);
            players[i - 1] = player;
            player.GetComponent<PlaceObjects>().SetGhostList(Placeables);
            foreach (GameObject ghost in Placeables)
            {
                Instantiate(ghost, Vector3.up, Quaternion.identity, player.transform);
            }
        }
    }

    void Update()
    {
        
    }
}