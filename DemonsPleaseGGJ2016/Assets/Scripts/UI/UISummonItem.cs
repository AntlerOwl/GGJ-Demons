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
    private Tooltip tooltip;

    void Awake()
    {
        summoningManager = GameManager.instance.SummoningManager;
        tooltip = GetComponent<Tooltip>();
    }

    void Start()
    {
        UpdateTooltip();
    }

	protected override void OnEndDrag()
    {
        ingredient = UIDragManager.instance.curIngredient;
        iconImage.sprite = ingredient.icon;
        summoningManager.AddIngredient(ingredient, summonSlotId);

        UpdateTooltip();

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

    public void UpdateTooltip()
    {
        string title = "";
        string flavor = "";
        if (ingredient)
        {
            title = ingredient.ingredientName;
            flavor = ingredient.flavorText;
        }
        print("Updating tooltip");
        tooltip.SetTipActive(ingredient);
        tooltip.SetTooltipText(title, flavor);
    }
}
