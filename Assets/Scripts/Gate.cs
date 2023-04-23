using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : CollidableObject
{
    [SerializeField] PlotOfLand landOwned;
    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        GameManager.instance.AddPlotOfland(landOwned);
        Destroy(gameObject);
    }
}
