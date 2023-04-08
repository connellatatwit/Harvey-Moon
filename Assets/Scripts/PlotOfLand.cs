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
    [SerializeField] List<Transform> spawnLocations;


}
