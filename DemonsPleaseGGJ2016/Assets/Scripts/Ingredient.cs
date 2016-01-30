using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour
{
    public string ingredientName = "Ingredient";
    public int cost = 0;
    public string tooltip = "Tooltip";
    public Sprite icon;

    [Tooltip("A type gameobject and a value for the tier of this ingredient")]
    public TypeTier typeTier;

}
