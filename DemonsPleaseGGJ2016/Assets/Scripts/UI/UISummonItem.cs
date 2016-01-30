using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISummonItem : UIEndDrag, IDragHandler, IBeginDragHandler
{
    public Ingredient ingredient;
    public Image iconImage;
    private SummoningManager summoningManager;
    [HideInInspector]public int summonSlotId;

    void Awake()
    {
        summoningManager = GameManager.instance.SummoningManager;
    }

	protected override void OnEndDrag()
    {
        ingredient = UIDragManager.instance.curIngredient;
        iconImage.sprite = ingredient.icon;
        summoningManager. AddIngredient(ingredient, summonSlotId);

        UIDragManager.instance.EndDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIDragManager.instance.SetPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // report to manager that we are dragging from summons
        UIDragManager.instance.BeginDrag(ingredient, summonSlotId);
    }
}
