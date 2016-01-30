using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningTable : MonoBehaviour
{
    public List<TypeTier> ingredients;
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
        List<TypeTier> mergedIngredients = MergeTypes(ingredients);
        foreach (var recipe in recipes)
        {
            List<TypeTier> recipeIngredients = MergeTypes(recipe.ingredients);
            bool matching = MatchingIngredients(mergedIngredients, recipeIngredients);
            if (matching) 
            {
                print("Ingredients matching: " + recipe.recipeName);
                return recipe;
            }
        }

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

    List<TypeTier> MergeTypes(List<TypeTier> orig)
    {
        List<TypeTier> merged = new List<TypeTier>();
        Dictionary<ItemType, int> mer = new Dictionary<ItemType, int>();
        for (int i = 0; i < orig.Count; i ++)
        {
            if (mer.ContainsKey(orig[i].type))
            {
                mer[orig[i].type] += orig[i].tier;
            }
            else
            {
                mer.Add(orig[i].type, orig[i].tier);
            }
            /*for (int j = 0; j < orig.Count; j ++)
            {
                if (orig[i].type == orig[j].type)
                {
                    if (mer.ContainsKey(
                }
            }*/
        }
        return merged;
    }

    /// <summary>
    /// Checks if the ingredients in the two lists match. Check both the types and wheter A's tier is more than B's tier.
    /// </summary>
    /// <returns><c>true</c>, if ingredients was matchinged, <c>false</c> otherwise.</returns>
    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    bool MatchingIngredients(List<TypeTier> a, List<TypeTier> b)
    {
        // Go through all items and compare them to the other items
        foreach (var itemA in a)
        {
            foreach (var itemB in b) 
            {
                // Check if the item types match
                if (itemA.type == itemB.type)
                {
                    // If they match, return false if A's tier is less than B's tier
                    if (itemA.tier < itemB.tier)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }
}
