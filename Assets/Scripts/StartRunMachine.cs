using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRunMachine : CollidableObject
{
    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        Debug.Log("Interacted with Start Machine!");
        GameManager.instance.StartRun();
    }
}
