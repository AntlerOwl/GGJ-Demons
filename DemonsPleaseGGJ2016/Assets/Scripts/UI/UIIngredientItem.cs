using UnityEngine;
using System.Collections;

public class UIIngredientItem : UIStartDrag
{
    public Ingredient ingredient;

    void Start()
    {
        if (ingredient)
            image = ingredient.icon;
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
