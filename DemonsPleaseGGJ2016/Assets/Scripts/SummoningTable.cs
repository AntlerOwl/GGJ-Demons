using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningTable : MonoBehaviour
{
    public const int MaxTier = 4;
    public List<Ingredient> ingredients;
    public List<Recipe> recipes;
    public List<Ingredient> allIngredients = new List<Ingredient>();

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
    [SerializeField]List<TypeTier> summoningIngredients;
    [SerializeField]List<TestListIngredient> recipeIngredients;
    public List<Recipe> matchingRecipes = new List<Recipe>();
    /// <summary>
    /// Tries to combine the ingredients.
    /// </summary>
    /// <returns>The best matching recipe to the ingredients. Null if no match.</returns>
    Recipe TryCombine()
    {
        matchingRecipes = new List<Recipe>();
        summoningIngredients = MergeIngredients(ingredients);
        recipeIngredients = new List<TestListIngredient>();
        int i = 0; // DEBUG
        foreach (var recipe in recipes)
        {
            print("Checking recipe " + recipe.recipeName);
            List<TypeTier> ttlist = MergeIngredients(recipe.ingredients); // Not debug, rename ttlist to recipeIngredients.

            // Just to see if it merges correctly
            recipeIngredients.Add(new TestListIngredient());
            recipeIngredients[i].ingredients = ttlist;
            i++;



            if (ttlist.Count > summoningIngredients.Count) 
            {
                print("There aren't enough ingredients for this recipe, skipping to next");
                continue;
            }

            bool matching = MatchingIngredients(summoningIngredients, ttlist);
            if (matching) 
            {
                print("Ingredients matching: " + recipe.recipeName);
//                return recipe;
                matchingRecipes.Add(recipe);
            }
        }
        if (matchingRecipes.Count > 0)
        {
            return matchingRecipes[0];
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

    List<TypeTier> MergeIngredients(List<Ingredient> orig)
    {
        List<TypeTier> merged = new List<TypeTier>();
        Dictionary<ItemType, int> mer = new Dictionary<ItemType, int>();
        for (int i = 0; i < orig.Count; i ++)
        {
            if (mer.ContainsKey(orig[i].typeTier.type))
            {
                mer[orig[i].typeTier.type] += orig[i].typeTier.tier;
            }
            else
            {
                mer.Add(orig[i].typeTier.type, orig[i].typeTier.tier);
            }
//            print(string.Format("Merged {0} has now tier {1}", orig[i].typeTier.type, orig[i].typeTier.tier));
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
//            print("Added merged: " + tt.type.typeName + "[" + tt.tier + "]" + UnityEditor.EditorApplication.timeSinceStartup);
        }
        return merged;
    }

    /// <summary>
    /// Checks summoningIngredients against checkIngredients. Check both the types and wheter summoning's tier is more than recipe's tier.
    /// </summary>
    /// <returns><c>true</c>, if ingredients was matchinged, <c>false</c> otherwise.</returns>
    /// <param name="a">The alpha component.</param>
    /// <param name="b">The blue component.</param>
    bool MatchingIngredients(List<TypeTier> summoningIngredients, List<TypeTier> recipeIngredients)
    {
        int matchingCount = 0;
        // Go through all items and compare them to the other items
        foreach (var summoningIng in summoningIngredients)
        {
            foreach (var recipeIng in recipeIngredients) 
            {
                // Check if the item types match
                if (summoningIng.type == recipeIng.type)
                {
                    // If they match, return true if summoning's tier is >= recipe's tier
                    if (summoningIng.tier >= recipeIng.tier)
                    {
//                        return true;
                        matchingCount ++;
                    }
                    else
                    {
                        print(string.Format("{0} Tier is less than {1} tier", summoningIng, recipeIng));
                        return false;
                    }
                }
                else
                {
                    print(string.Format("{0} doesn't match {1}", summoningIng.type, recipeIng.type));
//                    return false;
//                    matchingCount --;
                    continue;
                }
            }
        }
        print("matchingcount: " + matchingCount);
        if (matchingCount >= recipeIngredients.Count) return true;
        return false;
    }

    /// <summary>
    /// Gets an existing TypeTier object with the specified type and tier. Null if no matching is found.
    /// </summary>
    /// <returns>The type tier by type.</returns>
    /// <param name="type">Type.</param>
    /// <param name="tier">Tier.</param>
    Ingredient GetTypeTierByType(ItemType type, int tier)
    {
        tier = Mathf.Clamp(tier, 0, MaxTier); // Make sure we don't try to find a tier above the max tier availible
        foreach (var item in allIngredients)
        {
            if (item.typeTier.type == type && item.typeTier.tier == tier)
            {
                print(string.Format("{0}({3}) matching {1}({2})", type, item.typeTier.type, item.typeTier.tier, tier));
                return item;
            }
        }
        print(string.Format("couldnt find a matching typetier with {0}({1})", type, tier));
        return null;
    }
}

[System.Serializable]
public class TestListIngredient
{
    public List<TypeTier> ingredients;
}