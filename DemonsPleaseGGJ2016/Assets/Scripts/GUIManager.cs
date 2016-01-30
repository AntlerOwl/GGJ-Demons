using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Sprite emptySprite;
    [SerializeField]private GameObject uiIngredientPrefab;
    [SerializeField]private Transform ingredientsParent;
    private SummoningManager summoningManager;
    public static GUIManager instance;

    void Awake()
    {
        instance = this;
        summoningManager = GameManager.instance.SummoningManager;
    }

    void Start()
    {
        InitializeIngredientUI();
    }

    void InitializeIngredientUI()
    {
        foreach (var ingred in summoningManager.allIngredients)
        {
            GameObject obj = Instantiate(uiIngredientPrefab);
            obj.GetComponent<UIIngredientItem>().Init(ingred);
            obj.transform.SetParent(ingredientsParent);
        }
    }
}
