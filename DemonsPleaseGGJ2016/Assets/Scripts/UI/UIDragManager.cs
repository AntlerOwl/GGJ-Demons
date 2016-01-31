using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDragManager : MonoBehaviour
{
    public static UIDragManager instance;
    public Image dragImage;
    public Ingredient curIngredient;
    private int fromSummonSlot = -1;
    private SummoningManager summoningManager;

    void Awake()
    {
        summoningManager = GameManager.instance.SummoningManager;
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
                if (fromSummonSlot > -1)
                {
                    summoningManager.RemoveIngredient(fromSummonSlot);
                }
                EndDrag();
            }
        }
    }

    public void BeginDrag(Ingredient ingredient, int fromSummonSlot = -1)
    {
        

        dragImage.gameObject.SetActive(true);
        dragImage.sprite = ingredient.icon;
        curIngredient = ingredient;
        this.fromSummonSlot = fromSummonSlot;
    }

    public void EndDrag()
    {
        dragImage.gameObject.SetActive(false);
        StartCoroutine(ResetIngredientAndSlot());
    }

    IEnumerator ResetIngredientAndSlot()
    {
        yield return new WaitForEndOfFrame();
        curIngredient = null;
        fromSummonSlot = -1;
    }

    public void SetPosition()
    {
        dragImage.transform.position = Input.mousePosition;
    }
}
