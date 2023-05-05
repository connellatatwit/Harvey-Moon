using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : CollidableObject
{
    [SerializeField] PlotOfLand landOwned;
    [SerializeField] int cost;
    [SerializeField] float timerIncrease;

    [HideInInspector]
    public bool firstGate = false;
    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        if (!firstGate)
        {
            GameManager.instance.WriteMessage("Opening gates gives more time to harvest crops and opens up new possibilities", "Narrator");
            Gate[] gates = FindObjectsOfType(typeof(Gate)) as Gate[];
            for (int i = 0; i < gates.Length; i++)
            {
                gates[i].firstGate = true;
            }
        }

        if (cost <= GameManager.instance.GetBucketMoney())
        {
            GameManager.instance.SpendMoney(cost);
            GameManager.instance.AddPlotOfland(landOwned);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().IncreaseMoon(timerIncrease);
            Destroy(gameObject);
        }
    }
}
