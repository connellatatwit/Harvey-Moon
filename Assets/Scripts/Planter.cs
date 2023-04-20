using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    [SerializeField] List<GameObject> seeds;
    [SerializeField] int maxCrops;
    [SerializeField] int minCrops;

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
}
