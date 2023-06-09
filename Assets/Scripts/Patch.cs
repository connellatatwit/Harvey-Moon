using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour
{
    [SerializeField] List<Transform> spawnLocations;
    private List<Transform> spawnLocationsPresent = new List<Transform>();

    private void Start()
    {
        if(spawnLocations.Count == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                spawnLocations.Add(transform.GetChild(i));
            }
        }
    }
    public void InitPatch()
    {
        spawnLocationsPresent.Clear();

        for (int i = 0; i < spawnLocations.Count; i++)
        {
            spawnLocationsPresent.Add(spawnLocations[i]);
        }
    }

    public Transform GetSpawn()
    {
        int rand = Random.Range(0, spawnLocationsPresent.Count);
        Transform temp = spawnLocationsPresent[rand];
        spawnLocationsPresent.RemoveAt(rand);
        return temp;
    }
    public int Spawns()
    {
        return spawnLocations.Count;
    }
}
