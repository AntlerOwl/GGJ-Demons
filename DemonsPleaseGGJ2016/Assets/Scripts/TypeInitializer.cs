using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TypeInitializer : MonoBehaviour
{
    [SerializeField]private GameObject itemTypePrefab;
    [SerializeField]private GameObject typeTierPrefab;
    [SerializeField]private GameObject ingredientPrefab;
    [SerializeField]private TextAsset textAsset;
    public List<ItemType> itemTypes = new List<ItemType>();
    public List<TypeTier> typeTiers = new List<TypeTier>();

    void Start()
    {
        InitializeTypes();
    }

    void InitializeTypes()
    {
        List<TypeTier> tempTypeTiers = new List<TypeTier>();
        string[] lines = textAsset.text.Split('\n');
        string[] types = lines[0].Split(',');
        for (int i = 1; i < types.Length; i ++) // Start at 1 to skip the first empty cell
        {
            GameObject objType = Instantiate(itemTypePrefab);
            objType.name = "Type_" + types[i];
            ItemType itemType = objType.GetComponent<ItemType>();
            itemType.Init(types[i]);
            itemTypes.Add(itemType);

            // make TypeTiers here for each tier
            for (int j = 1; j <= 4; j++)
            {
                GameObject objTypeTier = Instantiate(typeTierPrefab);
                objTypeTier.name = "TypeTier_" + j + "-" + itemType.typeName;
                TypeTier typeTier = objTypeTier.GetComponent<TypeTier>();
                typeTier.Init(j, itemType);
                tempTypeTiers.Add(typeTier);
            }
        }

        typeTiers = new List<TypeTier>(tempTypeTiers);
        for (int i = 0; i < tempTypeTiers.Count; i++)
        {
            
        }

        for (int i = 1; i < 4; i++)
        {
            string[] items = lines[i].Split(',');
            for(int j = 1; j < items.Length; j ++) // Start at 1 to skip the first number cell
            {
                if (items[i].Length <= 0) continue;

                // Make ingredients
                GameObject obj = Instantiate(ingredientPrefab);
                obj.name = "Ing_" + items[j];
                Ingredient ingredient = obj.GetComponent<Ingredient>();
//                ingredient.Init(
            }
        }
    }
}
