using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonShrine : CollidableObject
{
    [SerializeField] Stats targetStat;
    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        Store.instance.UpgradePlayer(targetStat);
    }
}
