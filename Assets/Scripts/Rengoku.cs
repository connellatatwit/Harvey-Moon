using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rengoku : CollidableObject
{
    [Header("Convo 1")]
    [TextArea(3,3)]
    [SerializeField] List<string> conversation1Words;
    [SerializeField] List<string> conversation1Names;
    [Header("Convo 2")]
    [TextArea(3, 3)]
    [SerializeField] List<string> conversation2Words;
    [SerializeField] List<string> conversation2Names;

    [SerializeField] GameObject newHoverText;
    private bool firstouch = true;

    private int currentTalk = 0;

    private GameObject player;
    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UpdatePerson(hitObj);
            OnInteract();
        }
    }

    private void UpdatePerson(GameObject player)
    {
        this.player = player;
        currentTalk = player.GetComponent<MissionTracker>().GetQuestProgress("Purgatory");
    }

    protected virtual void OnInteract()
    {
        if(currentTalk == 0)
        {
            GameManager.instance.WriteMessage(conversation1Words, conversation1Names);
            player.GetComponent<MissionTracker>().ProgressQuest("Purgatory");
        }
        else if(currentTalk == 1)
        {
            GameManager.instance.WriteMessage(conversation2Words, conversation2Names);
        }
    }
}
