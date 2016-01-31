using UnityEngine;
using System.Collections;

public class DebugController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GameManager.instance.ChangeTotalMoney(250);
        }
    }
}
