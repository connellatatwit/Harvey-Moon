using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTracker : MonoBehaviour
{
    [Header("Purgatory Questline")]
    [SerializeField] int purgatoryQuest;


    public void ProgressQuest(string questLineName)
    {
        if(questLineName == "Purgatory")
        {
            purgatoryQuest++;
        }
    }

    public int GetQuestProgress(string questLineName)
    {
        if (questLineName == "Purgatory")
        {
            return purgatoryQuest;
        }
        else
            return -1;
    }
}
