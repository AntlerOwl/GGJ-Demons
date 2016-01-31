using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIIngredientItem : UIStartDrag
{
    public Ingredient ingredient;
    [SerializeField]private Image iconImage;
    [SerializeField]private Text costText;
    private Tooltip tooltip;

    public void Init(Ingredient ingred)
    {
        if (!tooltip) tooltip = GetComponent<Tooltip>();

        ingredient = ingred;
        if (ingredient)
        {
            image = ingredient.icon;
            iconImage.sprite = image;
            costText.text = "$" + ingredient.cost;
            GetComponent<Tooltip>().SetTooltipText(ingredient.ingredientName, ingredient.flavorText);
        }
        else
        {
            iconImage.sprite = GUIManager.instance.emptySprite;
        }
        costText.gameObject.SetActive(ingredient);
        if (tooltip)
        {
            tooltip.SetTipActive(ingredient);
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
