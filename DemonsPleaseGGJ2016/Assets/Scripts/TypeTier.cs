using UnityEngine;
using System.Collections;

[System.Serializable]
public class TypeTier
{
    [Tooltip("The type gameobject for this typetier")]
    public ItemType type;
    [Range(1, 4)]public int tier = 1;

    public TypeTier(ItemType type, int tier)
    {
        this.type = type;
        this.tier = tier;
    }
}
