using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : CollidableObject
{
    [Header("Ui stuff")]
    [SerializeField] GameObject seederUi;
    [SerializeField] Transform equippedSeedParent;
    [SerializeField] Transform seedPacketParent;

    [Header("Stuff")]
    [SerializeField] List<GameObject> seeds;
    [SerializeField] int maxCrops; // Spawn rate
    [SerializeField] int minCrops;
    [SerializeField] int maxSeedTypes; // Max amount of different seeds you can have.
    [SerializeField] int currentCost;

    private PlayerMovement player;

    public List<GameObject> GetSeeds()
    {
        return seeds;
    }

    public int MaxCrops
    {
        get { return maxCrops; }
    }
    public int MinCrops
    {
        get { return minCrops; }
    }
    public int CurrentCost
    {
        get { return currentCost; }
    }
    public int MaxTypes
    {
        get { return maxSeedTypes; }
    }

    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            hitObj.GetComponent<PlayerMovement>().MoonDown(true);
            player = hitObj.GetComponent<PlayerMovement>();
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        seederUi.SetActive(true);
        GameManager.instance.UpdatePlanter();
    }

    public void EndSeeder()
    {
        seederUi.SetActive(false);
        player.MoonDown(false);
    }

    public bool AddSeed(GameObject newSeed, Transform imageChild)
    {
        if(seeds.Count < maxSeedTypes)
        {
            seeds.Add(newSeed);
            imageChild.SetParent(equippedSeedParent);
            GameManager.instance.UpdatePlanter();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveSeed(GameObject seedPrefab, Transform imageChild)
    {
        if (seeds.Contains(seedPrefab))
        {
            seeds.Remove(seedPrefab);
            imageChild.SetParent(seedPacketParent);
            GameManager.instance.UpdatePlanter();
        }
    }

    public void UpgradePlanter()
    {
        //TODO Change how upgrades work really. Ex. How cost ramps up, how seed types go up, how seeds grow etc.
        maxSeedTypes += 1;
        currentCost += 10;
        GameManager.instance.UpdatePlanter();
    }
}
