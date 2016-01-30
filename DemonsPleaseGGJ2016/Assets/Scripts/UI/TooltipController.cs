using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
	[SerializeField]private RectTransform tooltipPanel; // The tooltip panel
	[SerializeField]private float tooltipDelay = 0.3f;
	private Text titleText; // The tooltip title text object
	private Text descText; // The tooltip title text object
	[SerializeField]private float tooltipMargin = 8f; // The margin for the tooltip panel
	private bool isActive = false; // Wheter the tooltip should be active or not
	private float tooltipTimer = -10f;

	void Awake()
	{
		titleText = tooltipPanel.GetChild(0).GetComponent<Text>();
		descText = tooltipPanel.GetChild(1).GetComponent<Text>();
		SetTooltipActive(false);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) OnDeactivate();

		if (isActive)
		{
			SetPosition();
		}
		else
		{
			if (tooltipTimer > -1f)
			{
				if (tooltipTimer < tooltipDelay)
				{
					tooltipTimer += Time.deltaTime;
				}
				else
				{
					tooltipTimer = -10f;
					SetTooltipActive(true);
					SetPosition();
				}
			}
		}
	}

	/// <summary>
	/// Called when hovering over the element with the Tooltip component.
	/// </summary>
	/// <param name="title">Title.</param>
	/// <param name="desc">Description.</param>
	public void OnActivate(string title, string desc)
	{
		OnDeactivate();
		titleText.text = title;
		descText.text = desc.Replace("\\", "\n");
		tooltipTimer = 0f;
	}

	/// <summary>
	/// Called when hovering over the element with the Tooltip component.
	/// </summary>
	public void OnDeactivate()
	{
		tooltipTimer = -10f;
		SetTooltipActive(false);
	}

	void SetTooltipActive(bool active)
	{
		isActive = active;
		tooltipPanel.gameObject.SetActive(active);
	}

	void SetPosition()
	{
		Vector3 mousePos = Input.mousePosition;
		Vector3 pos = new Vector3(mousePos.x + tooltipMargin, mousePos.y - tooltipMargin, 0f);

		// Clamp position
		float tooltipWidth = tooltipPanel.rect.width;
		float tooltipHeigth = tooltipPanel.rect.height;
		pos.x = Mathf.Clamp (pos.x, 0f, Screen.width - tooltipWidth);
		pos.y = Mathf.Clamp (pos.y, tooltipHeigth, Screen.height);

		// Set poisiton
		tooltipPanel.position = pos;
	}

    public void SetTextsActive(bool titleActive, bool descActive)
    {
        titleText.gameObject.SetActive(titleActive);
        descText.gameObject.SetActive(descActive);
    }
}
