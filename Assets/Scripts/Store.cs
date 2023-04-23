using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stats
{
    Speed,
    Moonlife
}

public class Store : MonoBehaviour
{
    [Header("Store Costs")]
    [SerializeField] int baseSpeedCost;
    private int speedCoste;
    [SerializeField] int baseMoonCost;
    private int moonCost;
    [Header("Upgrade Values")]
    [SerializeField] int baseSpeedUpgrade;
    private int speedUpgrade;
    [SerializeField] int baseMoonUpgrade;
    private int moonUpgrades;

    [SerializeField] Bucket bucket;

    private PlayerStats playerStats;

    public static Store instance;

    private void Awake()
    {
        if(instance != null){
            return;
        }
        instance = this;
    }

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        
        speedCoste = baseSpeedCost;
        moonCost = baseMoonCost;
        speedUpgrade = baseSpeedUpgrade;
        moonUpgrades = baseMoonUpgrade;
    }
    public void UpgradePlayer(Stats targetStat)
    {
        int cost = 0;
        int value = 0;
        if (targetStat == Stats.Speed)
        {
            value = speedUpgrade;
            cost = speedCoste;
        }
        else if (targetStat == Stats.Moonlife)
        {
            value = moonUpgrades;
            cost = moonCost;
        }
        if (bucket.Money < cost)
        {
            return;
        }
        bucket.SpendMoney(cost);
        GameManager.instance.UpdateUI();

        playerStats.UpgradeStat(targetStat, value);

    }
}
