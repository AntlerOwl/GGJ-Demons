using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMissionParameterItem : MonoBehaviour
{
    [SerializeField]private Text demonNameText;
    [SerializeField]private Slider progressSlider;

    public void Init(string title, int max, bool active)
    {
        demonNameText.gameObject.SetActive(active);
        progressSlider.gameObject.SetActive(active);
        demonNameText.text = title;
        UpdateSlider(0, max);
    }

    public void UpdateSlider(int cur, int max)
    {
        progressSlider.value = cur;
        progressSlider.maxValue = max;
    }
}
