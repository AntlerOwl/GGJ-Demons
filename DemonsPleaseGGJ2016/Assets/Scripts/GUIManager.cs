using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Sprite emptySprite;
    [SerializeField]private Text totalSummonCostText;
    [SerializeField]private Text totalMoneyText;
    [SerializeField]private GameObject uiIngredientPrefab;
    [SerializeField]private Transform ingredientsParent;
    [SerializeField]private RewardText origRewardText;
    [SerializeField]private Button summonButton;
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

    public void OnMoneyChange(int amt)
    {
        string prefix = "+";
        Color color = Color.green;
        if (amt < 0)
        {
            prefix = "";
            color = Color.red;
        }
        totalMoneyText.text = "Total money: $" + GameManager.instance.totalMoney;
        // TODO Display textlabel with amt, prefix and color
        PopText(prefix + amt, color);
        UpdateSummonButtonActive();
    }

    private List<RewardText> rTexts = new List<RewardText>();
    void PopText(string text, Color color)
    {
        GameObject obj = Instantiate(origRewardText.gameObject);
        RewardText rText = obj.GetComponent<RewardText>();
        rText.Initialize(text, color, totalMoneyText.transform.position);
        rText.transform.SetParent(origRewardText.transform.parent);
        obj.SetActive(true);
    }

    public void DestroyRewardText(RewardText rText)
    {
        rTexts.Remove(rText);
        Destroy(rText.gameObject);
    }

    public void UpdateTotalSummonCostDisplay(int totalCost)
    {
        totalSummonCostText.text = "Total cost: $" + totalCost;
        UpdateSummonButtonActive();
    }

    public void UpdateSummonButtonActive()
    {
        summonButton.interactable = (summoningManager.TotalCost <= GameManager.instance.totalMoney);
    }
}
