﻿    using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string title = "Title";
	[Tooltip("Use the character '\\' to get a newline")]
	[SerializeField]private string desc = "Description";
	private TooltipController tooltipController;
    private bool isActive = true;

	void Awake()
	{
		tooltipController = GameManager.instance.TooltipController;
	}

    public void SetTipActive(bool active)
    {
        isActive = active;
    }

	public void OnPointerEnter(PointerEventData e)
	{
        if (!isActive) return;

        tooltipController.SetTextsActive(title.Length > 0, desc.Length > 0);
		tooltipController.OnActivate(title, desc);
	}

	public void OnPointerExit(PointerEventData e)
	{
		tooltipController.OnDeactivate();
	}

	/// <summary>
	/// Sets the tooltip text and description.
	/// </summary>
	/// <param name="title">Title.</param>
	/// <param name="desc">Description.</param>
	public void SetTooltipText(string title, string desc)
	{
		this.title = title;
		this.desc = desc;
	}
}