﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SummoningManager : MonoBehaviour
{
    [SerializeField]private int upgradeSummoningCost = 4000;
    [SerializeField]private UISummonPanel summonPanel;
    public List<Ingredient> ingredientSlots;
    public List<Recipe> allRecipes;
    public List<Ingredient> allIngredients = new List<Ingredient>();
    public List<ItemType> allTypes = new List<ItemType>();
    public const int MaxTier = 4;
    private List<UISummonItem> summonThreeSlots = new List<UISummonItem>();
    private List<UISummonItem> summonFiveSlots = new List<UISummonItem>();
    private List<UISummonItem> summonSlots = new List<UISummonItem>();
    [SerializeField]private Transform summonSlotsParent;
    [SerializeField]private Transform summonThreeSlotsParent;
    [SerializeField]private Transform summonFiveSlotsParent;
    [SerializeField]private Image summonCircleImage; 
    [SerializeField]private Sprite[] summonCircleSprites;
    [SerializeField]private Text upgradeSummonCostText;
    [SerializeField]private Button upgradeSummonsButton;
    [SerializeField]private Demon[] allDemons;
    private MissionControll missionControll;

    public int TotalCost { 
        get { return totalCost; } 
        set { totalCost = value; GUIManager.instance.UpdateTotalSummonCostDisplay(totalCost); }
    }
    private int totalCost;

    void Awake()
    {
        for (int i = 0; i < allDemons.Length; i++)
        {
            allDemons[i].demonId = i;
            allDemons[i].hasSummoned = false;
        }

        missionControll = GameManager.instance.MissionControll;

        for (int i = 0; i < allTypes.Count; i++)
        {
            GameObject obj = Instantiate(allTypes[i].gameObject);
            ItemType type = obj.GetComponent<ItemType>();
            type.ID = i;
            allTypes[i] = type;
        }

        for (int i = 0; i < allIngredients.Count; i ++)
        {
            if (!allIngredients[i]) continue;

            GameObject obj = Instantiate(allIngredients[i].gameObject);
            Ingredient ing = obj.GetComponent<Ingredient>();
            allIngredients[i] = ing;
        }

        foreach (var ingredient in allIngredients)
        {
            if (!ingredient) continue;

            foreach (var type in allTypes)
            {
//                if (ingredient == null) print("ing == null");
//                if (ingredient.typeTier == null) print("ing.typetier == null");
//                if (ingredient.typeTier.type == null) print("ing.typetier.type == null");
//                if (type == null) print("type == null");

                if (ingredient.typeTier.type.typeName == type.typeName)
                {
                    ingredient.typeTier.type = type;
                }
            }
        }

        for (int i = 0; i < allRecipes.Count; i++)
        {
            GameObject obj = Instantiate(allRecipes[i].gameObject);
            Recipe recipe = obj.GetComponent<Recipe>();

            for (int j = 0; j < recipe.ingredients.Count; j++) 
            {
                foreach (var ingred in allIngredients)
                {
                    if (!ingred) continue;

                    if (recipe.ingredients[j].ingredientName == ingred.ingredientName)
                    {
                        recipe.ingredients[j] = ingred;
                    }
                }
            }

            allRecipes[i] = recipe;
        }
    }

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

//        summonSlots = new List<UISummonItem>(summonSlotsParent.GetComponentsInChildren<UISummonItem>());
//        for (int i = 0; i < summonSlots.Count; i++)
//        {
//            summonSlots[i].summonSlotId = i;
//            summonSlots[i].iconImage.sprite = GUIManager.instance.emptySprite;
//        }
        summonThreeSlots = new List<UISummonItem>(summonThreeSlotsParent.GetComponentsInChildren<UISummonItem>());
        summonFiveSlots = new List<UISummonItem>(summonFiveSlotsParent.GetComponentsInChildren<UISummonItem>());
        for (int i = 0; i < summonFiveSlots.Count; i++)
        {
            if (i < summonThreeSlots.Count)
            {
                summonThreeSlots[i].summonSlotId = i;
            }
            summonFiveSlots[i].summonSlotId = i;
        }

        SetSummonSlotGroup(false);

        upgradeSummonCostText.text = "$" + upgradeSummoningCost;
        UpdateUpgradeSummonsButtonInteractable();
    }

    public void OnSummonClick()
    {
        Recipe recipe = TryCombine();
        if (recipe != null)
        {
//            print("Results: " + recipe.target.demonName);

            GameManager.instance.ChangeTotalMoney(-TotalCost);

            missionControll.OnMadeDemon(recipe.target);
            summonPanel.Activate(recipe.target);

            // TODO Clear workbench
            ClearSummoningTable();
        }
        else
        {
            print("No matching recipes");
        }
    }

    public void OnUpgradeClick()
    {
        GameManager.instance.ChangeTotalMoney(-upgradeSummoningCost);
        SetSummonSlotGroup(true);
    }

    void SetSummonSlotGroup(bool fromThreeToFive)
    {
        ClearSummoningTable();
        if (fromThreeToFive)
        {
            summonSlots = new List<UISummonItem>();
            for (int i = 0; i < summonFiveSlots.Count; i++)
            {
                summonSlots.Add(summonFiveSlots[i]);
            }

            upgradeSummonsButton.gameObject.SetActive(false);
            upgradeSummonCostText.gameObject.SetActive(false);
        }
        else
        {
            summonSlots = new List<UISummonItem>();
            for (int i = 0; i < summonThreeSlots.Count; i++)
            {
                summonSlots.Add(summonThreeSlots[i]);
            }
        }
        summonThreeSlotsParent.gameObject.SetActive(!fromThreeToFive);
        summonFiveSlotsParent.gameObject.SetActive(fromThreeToFive);
        summonCircleImage.sprite = (fromThreeToFive) ? summonCircleSprites[1] : summonCircleSprites[0];
    }

    public void UpdateUpgradeSummonsButtonInteractable()
    {
        upgradeSummonsButton.interactable = upgradeSummoningCost <= GameManager.instance.totalMoney;
    }

    void ClearSummoningTable()
    {
        for (int i = 0; i < summonSlots.Count; i++)
        {
            RemoveIngredient(i);
        }
    }

    public void AddIngredient(Ingredient ingredient, int summonSlotId)
    {
        if (ingredientSlots[summonSlotId])
        {
            RemoveIngredient(summonSlotId);
        }

        ingredientSlots[summonSlotId] = ingredient;
        TotalCost += ingredient.cost;
        summonSlots[summonSlotId].iconImage.sprite = ingredient.icon;
    }

    public void RemoveIngredient(int summonSlotId)
    {
        if (!ingredientSlots[summonSlotId]) return; // Stop if there aren't any ingredients in this slot

        TotalCost -= ingredientSlots[summonSlotId].cost;
        ingredientSlots[summonSlotId] = null;
        summonSlots[summonSlotId].iconImage.sprite = GUIManager.instance.emptySprite;
        summonSlots[summonSlotId].UpdateTooltip();
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
