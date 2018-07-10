using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Does nothing but store the placable items for a particular level.
 * Must be placed on a LevelItemRestrictor prefab
 * If the player is to have all availible items for a level, this script nor its parent object are needed.
 */
public class LevelItemRestrictor : MonoBehaviour
{
    [SerializeField]
    private List<PlaceableItem> levelItems;


    public IList<PlaceableItem> GetLevelItems()
    {
        return levelItems.AsReadOnly();
    }
}