using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningTable : MonoBehaviour
{
    public const int MaxTier = 4;
    public List<TypeTier> ingredients;
    public List<Recipe> recipes;
    public List<TypeTier> allTypeTiers = new List<TypeTier>();

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
        List<TypeTier> summoningIngredients = MergeTypes(ingredients);
        foreach (var recipe in recipes)
        {
            List<TypeTier> recipeIngredients = MergeTypes(recipe.ingredients);
            bool matching = MatchingIngredients(summoningIngredients, recipeIngredients);
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
//            print(string.Format("Merged {0} has now tier {1}", orig[i].type, orig[i].tier));
        }
        foreach (var item in mer)
        {
//            TypeTier tt = GetTypeTierByType(item.Key, item.Value);
//            if (tt)
//            {
//                
//                merged.Add(tt);
//            }
            TypeTier tt = new TypeTier(item.Key, item.Value);
            merged.Add(tt);
        }
        return merged;
    }

    /// <summary>
    /// Checks summoningIngredients against checkIngredients. Check both the types and wheter summoning's tier is more than check's tier.
    /// </summary>
    /// <returns><c>true</c>, if ingredients was matchinged, <c>false</c> otherwise.</returns>
    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    bool MatchingIngredients(List<TypeTier> summoningIngredients, List<TypeTier> checkIngredients)
    {
        // Go through all items and compare them to the other items
        foreach (var summoningIng in summoningIngredients)
        {
            foreach (var checkIng in checkIngredients) 
            {
                // Check if the item types match
                if (summoningIng.type == checkIng.type)
                {
                    // If they match, return false if A's tier is less than B's tier
                    if (summoningIng.tier < checkIng.tier)
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

    /// <summary>
    /// Gets an existing TypeTier object with the specified type and tier. Null if no matching is found.
    /// </summary>
    /// <returns>The type tier by type.</returns>
    /// <param name="type">Type.</param>
    /// <param name="tier">Tier.</param>
    TypeTier GetTypeTierByType(ItemType type, int tier)
    {
        tier = Mathf.Clamp(tier, 0, MaxTier); // Make sure we don't try to find a tier above the max tier availible
        foreach (var item in allTypeTiers)
        {
            if (item.type == type && item.tier == tier)
            {
                print(string.Format("{0}({3}) matching {1}({2})", type, item.type, item.tier, tier));
                return item;
            }
        }
        print(string.Format("couldnt find a matching typetier with {0}({1})", type, tier));
        return null;
    }
}
