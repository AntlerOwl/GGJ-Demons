using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummoningManager : MonoBehaviour
{
    [SerializeField]private UISummonPanel summonPanel;
    public List<Ingredient> ingredientSlots;
    public List<Recipe> allRecipes;
    [Tooltip("List of all ingredients availible")]
    public List<Ingredient> allIngredients = new List<Ingredient>();
    public const int MaxTier = 4;
    private List<UISummonItem> summonSlots = new List<UISummonItem>();
    [SerializeField]private Transform summonSlotsParent;

    void Start()
    {
        Init();
    }

    void Init()
    {
        ingredientSlots = new List<Ingredient>();
        for (int i = 0; i < 5; i++)
        {
            ingredientSlots.Add(null);
        }

        summonSlots = new List<UISummonItem>(summonSlotsParent.GetComponentsInChildren<UISummonItem>());
        for (int i = 0; i < summonSlots.Count; i++)
        {
            summonSlots[i].summonSlotId = i;
        }
    }

    public void OnSummonClick()
    {
        Recipe recipe = TryCombine();
        if (recipe != null)
        {
            print("Results: " + recipe.target.demonName);
            // TODO Sum up cost and see if we have enough
            int totalSum = 0;
            foreach (var item in ingredientSlots)
            {
                if (item)
                {
                    totalSum += item.cost;
                }
            }
            print("Total sum: " + totalSum);

            summonPanel.Activate(recipe.target.icon);
        }
        else
        {

            print("No matching recipes");
        }
    }

    public void AddIngredient(Ingredient ingredient, int summonSlotId)
    {
        ingredientSlots[summonSlotId] = ingredient;
    }

    public void RemoveIngredient(int summonSlot)
    {
        ingredientSlots[summonSlot] = null;
        summonSlots[summonSlot].iconImage.sprite = GUIManager.instance.emptySprite;
    }

    /// <summary>
    /// Tries to combine the ingredients.
    /// </summary>
    /// <returns>The best matching recipe to the ingredients. Null if no match.</returns>
    Recipe TryCombine()
    {
        List<Recipe> matchingRecipes = new List<Recipe>();
        List<Ingredient> curIngredients = new List<Ingredient>();
//        ingredientSlots.ForEach(x => curIngredients.Add(x));
        foreach (var item in ingredientSlots)
        {
            if (item)
            {
                curIngredients.Add(item);
            }
        }

        List<TypeTier> summoningIngredients = MergeIngredients(curIngredients);

        foreach (var recipe in allRecipes)
        {
            List<TypeTier> recipeIngredients = MergeIngredients(recipe.ingredients); // Not debug, rename ttlist to recipeIngredients.

            if (recipeIngredients.Count > summoningIngredients.Count) 
            {
//                print("There aren't enough ingredients for this recipe, skipping to next");
                continue;
            }

            bool ingredientsMatchingRecipe = MatchingIngredients(summoningIngredients, recipeIngredients);
            if (ingredientsMatchingRecipe) 
            {
//                print("Ingredients matching: " + recipe.recipeName);
                matchingRecipes.Add(recipe);
            }
        }
        if (matchingRecipes.Count > 0)
        {
            // TODO Figure out if this is better than just returning the first
            return matchingRecipes[Random.Range(0, matchingRecipes.Count)];
        }
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
                        print(string.Format("{0} {2} is less than {1} {3}", summoningIng.type, recipeIng.type, summoningIng.tier, recipeIng.tier));
//                        return false;
                        continue;
                    }
                }
                else
                {
//                    print(string.Format("{0} doesn't match {1}", summoningIng.type, recipeIng.type));
                    // Skip to next ingredient
                    continue;
                }
            }
        }
//        print("matchingcount: " + matchingCount);

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
