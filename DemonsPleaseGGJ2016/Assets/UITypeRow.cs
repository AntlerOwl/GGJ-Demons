using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UITypeRow : MonoBehaviour
{
    public ItemType type;
    public List<UIIngredientItem> ingredients = new List<UIIngredientItem>();

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject obj = Instantiate(GUIManager.instance.uiIngredientItemPrefab);
            ingredients.Add(obj.GetComponent<UIIngredientItem>());
            ingredients[i].Init(null);
            obj.transform.SetParent(transform);
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
//        print(type + "-" + ingredients.Count);
//        print("Adding with index " + (ingredient.typeTier.tier-1));
        ingredients[ingredient.typeTier.tier-1].Init(ingredient);
    }
}
