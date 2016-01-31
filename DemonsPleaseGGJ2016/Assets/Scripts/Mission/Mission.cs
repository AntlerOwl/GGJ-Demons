using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour
{
    public List<MissionParameter> targets = new List<MissionParameter>();
    public int reward = 0;
    public string missionName = "Mission";
    public string description = "Description";
    private MissionControll missionControll;

    void Awake()
    {
        missionControll = GameManager.instance.MissionControll;
    }

    public void InitializeMission()
    {
        //for (int i = 0; i < targets.Count; i++)
        for (int i = 0; i < missionControll.uiMissionParameterItems.Count; i++)
        {
            string title = "Title";
            int max = 0;
            if (i < targets.Count)
            {
                targets[i].uiMissionItem = missionControll.uiMissionParameterItems[i];
                title = targets[i].demon.demonName;
                max = targets[i].targetCount;

//                targets[i].uiMissionItem.Init(targets[i].demon.demonName, targets[i].targetCount);
            }
            missionControll.uiMissionParameterItems[i].Init(title, max, i < targets.Count);
        }
    }

    public void ResetMissionItems()
    {
        foreach (var target in targets)
        {
            target.uiMissionItem = null;
        }
    }

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

    /// <summary>
    /// Called when a demon is made. Returns true if the demon that was made is a part of the mission.
    /// </summary>
    /// <returns><c>true</c>, if demon was maded, <c>false</c> otherwise.</returns>
    /// <param name="demon">Demon.</param>
    public bool MadeDemon(Demon demon)
    {
        int targetId = CheckTargetForDemon(demon);
        if (targetId > -1)
        {
            targets[targetId].MadeDemon();
            // TODO Update UIMission
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
