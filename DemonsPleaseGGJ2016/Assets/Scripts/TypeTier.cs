using UnityEngine;
using System.Collections;

public class TypeTier : MonoBehaviour
{
    public ItemType type;
    [Range(1, 4)]public int tier = 1;

    public void Init(int tier, ItemType type)
    {
        this.type = type;
        this.tier = tier;
    }
}
