using UnityEngine;
using System.Collections;

public class ItemType : MonoBehaviour
{
    public string typeName = "Type";

    public void Init(string type)
    {
        typeName = type;
    }
}
