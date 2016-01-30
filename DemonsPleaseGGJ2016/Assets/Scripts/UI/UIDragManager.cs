using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDragManager : MonoBehaviour
{
    public static UIDragManager instance;
    public Image dragImage;
    public Ingredient curIngredient;

    void Awake()
    {
        if (instance)
        {
            Debug.Log("More than one instance of UIDragManager exists!", this);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if (curIngredient)
        {
            if (Input.GetMouseButtonUp(0))
            {
                EndDrag();
            }
        }
    }

    public void BeginDrag(Ingredient ingredient)
    {
        dragImage.gameObject.SetActive(true);
        dragImage.sprite = ingredient.icon;
        curIngredient = ingredient;
    }

    public void EndDrag()
    {
        print("End drag");
        dragImage.gameObject.SetActive(false);
        curIngredient = null;
    }

    public void SetPosition()
    {
        dragImage.transform.position = Input.mousePosition;
    }
}
