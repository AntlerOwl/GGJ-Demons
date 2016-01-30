using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string title = "Title";
	[Tooltip("Use the character '\\' to get a newline")]
	[SerializeField]private string desc = "Description";
	private TooltipController tooltipController;

	void Awake()
	{
		tooltipController = GameManager.instance.TooltipController;
	}

	public void OnPointerEnter(PointerEventData e)
	{
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
        tooltipController.SetTextsActive(title.Length > 0, desc.Length > 0);
	}
}