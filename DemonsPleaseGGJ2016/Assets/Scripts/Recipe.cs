using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : MonoBehaviour
{
    public string recipeName = "Recipe";
    [Tooltip("Demon to summon with the specified ingredients.")]
    public Demon target;
    [Tooltip("A list of all ingredients necessary to summon the target demon.")]
    public List<Ingredient> ingredients;
}
