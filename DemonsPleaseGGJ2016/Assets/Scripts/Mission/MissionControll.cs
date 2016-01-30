using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionControll : MonoBehaviour
{
    public List<Mission> currentMissions = new List<Mission>();
    public Mission curMission;
    public List<Mission> availibleMissions = new List<Mission>();

    public void GiveRandomMission()
    {
        // TODO Alert player of new mission
        GiveMission(availibleMissions[Random.Range(0, availibleMissions.Count)]);
    }

    public void GiveMission(Mission mission)
    {
        mission.ResetMission();
        currentMissions.Add(mission);
        availibleMissions.Remove(mission);
    }

    public void RemoveMission(Mission mission)
    {
//        int missionIndex = currentMissions.IndexOf(mission);
        availibleMissions.Add(mission);
        currentMissions.Remove(mission);
    }

    public void OnMadeDemon(Demon demon)
    {
        foreach (var mission in currentMissions)
        {
            bool demonMade = mission.MadeDemon(demon);
            if (demonMade)
            {
                if (mission.IsFinished())
                {
                    // TODO report that the mission is finished
                    RemoveMission(mission);
                }
            }
            else
            {
                continue;
            }
        }
    }
}
