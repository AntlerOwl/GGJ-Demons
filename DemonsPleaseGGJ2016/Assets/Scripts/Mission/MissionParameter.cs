using UnityEngine;
using System.Collections;

[System.Serializable]
public class MissionParameter
{
    public Demon demon;
    public int madeCount = 0;
    public int targetCount = 3;
    public bool IsDone { get { return madeCount >= targetCount; } }

    public void MadeDemon()
    {
        madeCount ++;
    }

    public void ResetTarget()
    {
        madeCount = 0;
    }
}
