using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIIngredientItem : UIStartDrag
{
    public Ingredient ingredient;
    [SerializeField]private Image iconImage;

    public void Init(Ingredient ingred)
    {
        ingredient = ingred;
        if (ingredient)
        {
            image = ingredient.icon;
            iconImage.sprite = image;
            GetComponent<Tooltip>().SetTooltipText(ingredient.ingredientName, ingredient.flavorText);
        }
    }

//    protected override void OnDrag() 
//    {
//        
//    }

    protected override void OnBeginDrag() 
    {
        UIDragManager.instance.BeginDrag(ingredient);
    }
}
