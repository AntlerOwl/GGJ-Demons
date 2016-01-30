using UnityEngine;
using System.Collections;

[System.Serializable]
public class TypeTier
{
    public ItemType type;
    [Range(1, 4)]public int tier = 1;

    public TypeTier(ItemType type, int tier)
    {
        this.type = type;
        this.tier = tier;
    }

    public void Init(int tier, ItemType type)
    {
        this.type = type;
        this.tier = tier;
    }
}
