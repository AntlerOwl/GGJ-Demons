using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISummonPanel : MonoBehaviour
{
    public Image summonImage;

    public void Activate(Sprite image)
    {
        summonImage.sprite = image;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
