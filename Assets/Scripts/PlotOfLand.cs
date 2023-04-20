using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotOfLand : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] string nameOfPlot;
    [SerializeField] int costOfPlot;
    [Header("Unity Stuff")]
    //[SerializeField] GameObject mainCropPrefab;
    [SerializeField] List<GameObject> possibleCrops;
    [SerializeField] List<Patch> patches;

    public void InitPlot()
    {
        int maxSpawns = 0;
        for (int i = 0; i < patches.Count; i++)
        {
            patches[i].InitPatch();
            maxSpawns = patches[i].Spawns();
            int spawns = Random.Range(maxSpawns / 2, maxSpawns);

            for (int j = 0; j < spawns; j++)
            {
                SpawnPlant(patches[i]);
            }
        }
    }
    public void InitPlot(List<GameObject> crops, int minCrops, int maxCrops)
    {
        possibleCrops = crops;
        for (int i = 0; i < patches.Count; i++)
        {
            patches[i].InitPatch();
            maxCrops = Mathf.Clamp(maxCrops, 0, patches[i].Spawns());
            minCrops = Mathf.Clamp(minCrops, 0, patches[i].Spawns());

            int spawns = Random.Range(minCrops, maxCrops);

            int rand = Random.Range(0, possibleCrops.Count);
            GameObject plant = possibleCrops[rand];
            for (int j = 0; j < spawns; j++)
            {
                SpawnPlant(patches[i], plant);
            }
        }
    }

    void SpawnPlant(Patch currentPatch)
    {
        int rand = Random.Range(0, possibleCrops.Count);
        Instantiate(possibleCrops[rand], currentPatch.GetSpawn().position, Quaternion.identity);
        //GameObject plant = Instantiate(mainCropPrefab, currentPatch.GetSpawn().position, Quaternion.identity);
    }
    void SpawnPlant(Patch currentPatch, GameObject plant)
    {
        Instantiate(plant, currentPatch.GetSpawn().position, Quaternion.identity);
        //GameObject plant = Instantiate(mainCropPrefab, currentPatch.GetSpawn().position, Quaternion.identity);
    }
}
