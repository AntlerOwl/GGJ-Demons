using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIIngredientList : MonoBehaviour
{
    public List<UITypeRow> typeRows = new List<UITypeRow>();
    private SummoningManager summoningManager;

    void Awake()
    {
        summoningManager = GameManager.instance.SummoningManager;
    }

    void Start()
    {
        for (int i = 0; i < summoningManager.allTypes.Count; i++)
        {
            GameObject obj = Instantiate(GUIManager.instance.uiTypeRowPrefab);
            UITypeRow row = obj.GetComponent<UITypeRow>();
            row.type = summoningManager.allTypes[i];
            typeRows.Add(row);
            obj.transform.SetParent(transform);
        }


    }

    [ContextMenu("Init")]
    void InitializeIngredients()
    {
        foreach (var ingredient in summoningManager.allIngredients)
        {
            AddIngredient(ingredient);
        }
    }

    public List<ItemType> rowTypes = new List<ItemType>();
    public List<ItemType> ingTypes = new List<ItemType>();

    public void AddIngredient(Ingredient ingredient)
    {
        //foreach (var item in typeRows)
        for (int i = 0; i < typeRows.Count; i ++)
        {
            rowTypes.Add(typeRows[i].type);
            ingTypes.Add(ingredient.typeTier.type);

            print(typeRows[i].type.typeName + "{" + typeRows[i].type.ID + "}" + " == " 
                + ingredient.typeTier.type.typeName + "(" + ingredient.typeTier.type.ID + "): " 
                + (typeRows[i].type.ID == ingredient.typeTier.type.ID));
            if (typeRows[i].type == ingredient.typeTier.type)
            //if (item.type.ID == ingredient.typeTier.type.ID)
//            if (typeRows[i].type.ID == ingredient.typeTier.type.ID)
            {
                typeRows[i].AddIngredient(ingredient);
            }
        }
    }
}
