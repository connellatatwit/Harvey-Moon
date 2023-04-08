using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    [SerializeField] List<Transform> spawnLocations;

    public Transform GetSpawn()
    {
        int rand = Random.Range(0, spawnLocations.Count);
        Transform temp = spawnLocations[rand];
        spawnLocations.RemoveAt(rand);
        return temp;
    }
    public int Spawns()
    {
        return spawnLocations.Count;
    }
}
