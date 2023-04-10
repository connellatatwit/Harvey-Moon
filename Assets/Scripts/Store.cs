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
    }
    public void UpgradePlayer(Stats targetStat, int value, int cost)
    {
        if (bucket.Money <= cost)
            return;

        playerStats.UpgradeStat(targetStat, value);

    }
}
