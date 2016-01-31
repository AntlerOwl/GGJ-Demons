using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int totalMoney = 0;
    [SerializeField]private GameObject sellSoulButton;
    public SummoningManager SummoningManager { private set; get; }
    public TooltipController TooltipController { private set; get; }
    public MissionControll MissionControll { private set; get; }
    public static GameManager instance;

    void Awake()
    {
        instance = this;
        SummoningManager = GetComponent<SummoningManager>();
        TooltipController = GetComponent<TooltipController>();
        MissionControll = GetComponent<MissionControll>();
    }

    void Start()
    {
        totalMoney = 0;
    }

    public void ChangeTotalMoney(int amt)
    {
        totalMoney += amt;
        GUIManager.instance.OnMoneyChange(amt);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSellSoulClick()
    {
        
    }
}
