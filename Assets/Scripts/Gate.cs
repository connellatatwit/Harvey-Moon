using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : CollidableObject
{
    [SerializeField] PlotOfLand landOwned;
    [SerializeField] int cost;
    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        if(cost <= GameManager.instance.GetBucketMoney())
        {
            GameManager.instance.SpendMoney(cost);
            GameManager.instance.AddPlotOfland(landOwned);
            Destroy(gameObject);
        }
    }
}
