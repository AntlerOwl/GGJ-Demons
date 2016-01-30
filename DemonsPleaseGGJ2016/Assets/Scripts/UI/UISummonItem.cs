using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISummonItem : UIEndDrag
{
    public Ingredient ingredient;
    public Image iconImage;
    private SummoningManager summoningManager;
    private int summonSlotId;

    void Awake()
    {
        summoningManager = GameManager.instance.SummoningManager;
        summonSlotId = transform.GetSiblingIndex();
    }

	protected override void OnEndDrag()
    {
        ingredient = UIDragManager.instance.curIngredient;
        iconImage.sprite = ingredient.icon;
        summoningManager.AddIngredient(ingredient, summonSlotId);

        UIDragManager.instance.EndDrag();
    }
}
