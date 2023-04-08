using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotOfLand : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] string nameOfPlot;
    [SerializeField] int costOfPlot;
    [Header("Unity Stuff")]
    [SerializeField] GameObject mainCropPrefab;
    [SerializeField] List<Patch> patches;

    private void Start()
    {
        InitPlot();
    }
    public void InitPlot()
    {
        int maxSpawns = 0;
        for (int i = 0; i < patches.Count; i++)
        {
            maxSpawns = patches[i].Spawns();
            int spawns = Random.Range(maxSpawns / 2, maxSpawns);

            for (int j = 0; j < spawns; j++)
            {
                SpawnPlant(patches[i]);
            }
        }


    }

    void SpawnPlant(Patch currentPatch)
    {
        GameObject plant = Instantiate(mainCropPrefab, currentPatch.GetSpawn().position, Quaternion.identity);
    }
}
