using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButton : MonoBehaviour
{
    [SerializeField] Stats targetStat;
    [SerializeField] int increaseAmount;
    [SerializeField] int cost;

    public int Cost
    {
        get { return cost; }
    }
    public int Amount
    {
        get { return increaseAmount; }
    }
    public Stats TargetStat
    {
        get { return targetStat; }
    }


    public void OnClick()
    {
        Store.instance.UpgradePlayer(targetStat, Amount, cost);
    }
}
