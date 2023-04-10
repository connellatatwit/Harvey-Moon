using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    [SerializeField] Stats targetStat;

    public void OnClick()
    {
        Store.instance.UpgradePlayer(targetStat);
    }
}
