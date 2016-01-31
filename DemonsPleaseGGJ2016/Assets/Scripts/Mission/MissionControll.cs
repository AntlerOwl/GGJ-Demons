using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionControll : MonoBehaviour
{
    [SerializeField]private float scanDelay = 2f;
    [Range(0f, 1f)][SerializeField]private float scanChance = 0.6f;
    public Mission curMission;
    public List<Mission> availibleMissions = new List<Mission>();
    public List<UIMissionParameterItem> uiMissionParameterItems;
    [SerializeField]private Transform uiMissionParameterParent;
    [SerializeField]private Text titleText;
    [SerializeField]private Text descText;
    [SerializeField]private Text rewardText;
    [SerializeField]private Text turnInText;
    [SerializeField]private GameObject discardButtonObject;
    [SerializeField]private GameObject missionDetailsObject;
    [SerializeField]private GameObject missionWaitObject;
    private Mission prevMission;

    void Start()
    {
        uiMissionParameterItems = 
            new List<UIMissionParameterItem>(uiMissionParameterParent.GetComponentsInChildren<UIMissionParameterItem>());

//        GiveMission(curMission);

        StartMissionScan();
    }

    void StartMissionScan()
    {
        StopMissionScan();
        InvokeRepeating("ScanForMission", scanDelay, scanDelay);
    }

    void StopMissionScan()
    {
        CancelInvoke("ScanForMission");
    }

    void ScanForMission()
    {
        float chance = Random.Range(0f, 1f);
        if (chance > scanChance)
        {
            GiveMission(GetRandomMission());
            StopMissionScan();
        }
    }

    public void GiveMission(Mission mission)
    {
        if (prevMission)
        {
            availibleMissions.Add(prevMission);
            prevMission = null;
        }

        mission.ResetMission();
        curMission = mission;
        availibleMissions.Remove(mission);
        curMission.InitializeMission();
        titleText.text = curMission.missionName;
        descText.text = curMission.description;
        rewardText.text = "Reward: $" + curMission.reward;

        discardButtonObject.SetActive(true);
        missionWaitObject.SetActive(false);
        missionDetailsObject.SetActive(true);
    }

    Mission GetRandomMission()
    {
        return availibleMissions[Random.Range(0, availibleMissions.Count)];
    }

    public void RemoveMission()
    {
        if (!curMission) return; // No current mission, nothing to remove

        // Just make sure we add the current mission to availiblemissions if there are no other, else add to prevmission
        if (availibleMissions.Count <= 0)
        {
            availibleMissions.Add(curMission);
        }
        else
        {
            prevMission = curMission;
        }
//        availibleMissions.Add(curMission);
        curMission = null;

        // TODO Disable mission screen
        // TODO Start ticking for new mission
        missionWaitObject.SetActive(true);
        missionDetailsObject.SetActive(false);
        StartMissionScan();
    }

    public void OnMadeDemon(Demon demon)
    {
        // Stop if we don't have a mission
        if (!curMission) return;

        bool madeDemonIsMission = curMission.MadeDemon(demon);
        if (madeDemonIsMission)
        {
            if (curMission.IsFinished())
            {
                // TODO report that the mission is finished
                //RemoveMission();
                print("Mission finiesed!");
                turnInText.text = string.Format("Turn in [${0}]", curMission.reward);
                discardButtonObject.SetActive(false);
            }
        }
    }

    public void OnTurnInClick()
    {
        GameManager.instance.ChangeTotalMoney(curMission.reward);
        RemoveMission();
    }

    public void OnDiscardClick()
    {
        RemoveMission();
    }
}
