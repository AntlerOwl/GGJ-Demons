using UnityEngine;
using System.Collections;

public class ItemType : MonoBehaviour
{
    public string typeName = "Type";
    public int ID = 0;

    public void Init(string type)
    {
        typeName = type;
    }

    public static bool operator ==(ItemType a, ItemType b)
    {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        return a.ID == b.ID;
    }

    public static bool operator !=(ItemType a, ItemType b)
    {
        return !(a == b);
    }
}
