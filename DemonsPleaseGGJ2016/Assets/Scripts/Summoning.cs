using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Summoning : MonoBehaviour
{
    public List<RecipeIngredient> ingredients;
    public List<Recipe> recipes;

    [ContextMenu("Test combine")]
    public void TestCombine()
    {
        Recipe recipe = TryCombine();
        if (recipe != null)
        {
            print("Results: " + recipe.target.demonName);
        }
        else
        {
            print("No mathing recipes");
        }
    }

    /// <summary>
    /// Tries to combine the ingredients.
    /// </summary>
    /// <returns>The best matching recipe to the ingredients. Null if no match.</returns>
    Recipe TryCombine()
    {
        // Find out what we have and how much
        // 


       /* if (ingredients.Count < 3) return null;

        foreach (var recipe in recipes) // Go through all recipies
        {
            bool matchingRecipe = true;
            foreach (var ingredient in ingredients) // Go through all ingredients in the summoner
            {
                // Check if the recipe contains the ingredient
                if (!recipe.recipeIngredients.Contains(ingredient))
                {
                    matchingRecipe = false;
                    continue;
                }
            }
            if (matchingRecipe)
            {
                return recipe;
            }
        }*/
        return null;
    }
}
