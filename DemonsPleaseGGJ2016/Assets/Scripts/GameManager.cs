using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int totalMoney = 100;

    public SummoningManager SummoningManager { private set; get; }
    public TooltipController TooltipController { private set; get; }
    public static GameManager instance;

    void Awake()
    {
        instance = this;
        SummoningManager = GetComponent<SummoningManager>();
        TooltipController = GetComponent<TooltipController>();
    }
}
