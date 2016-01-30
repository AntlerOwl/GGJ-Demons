using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public SummoningManager SummoningManager { private set; get; }
    public static GameManager instance;

    void Awake()
    {
        instance = this;
        SummoningManager = GetComponent<SummoningManager>();
    }
}
