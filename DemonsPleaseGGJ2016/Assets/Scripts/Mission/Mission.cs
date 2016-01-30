using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour
{
    public List<MissionParameter> targets = new List<MissionParameter>();
    public int reward = 0;
    public string missionName = "Mission";
    public string description = "Description";

    public bool IsFinished()
    {
        foreach (var target in targets)
        {
            // If any targets aren't done, return false
            if (!target.IsDone)
            {
                return false;
            }
        }
        return true;
    }

    public bool MadeDemon(Demon demon)
    {
        int targetId = CheckTargetForDemon(demon);
        if (targetId > -1)
        {
            targets[targetId].MadeDemon();
            return true;
        }
        return false;
    }

    public void ResetMission()
    {
        foreach (var target in targets)
        {
            target.ResetTarget();
        }
    }

    int CheckTargetForDemon(Demon demon)
    {
        for (int i = 0; i < targets.Count; i ++)
        {
            if (demon == targets[i].demon)
            {
                return i;
            }
        }
        return -1;
    }
}
