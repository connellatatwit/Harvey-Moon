using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform startLocation;
    [SerializeField] List<PlotOfLand> landPlots;
    [SerializeField] MoonTimer clock;

    [SerializeField] GameObject outGameUi;

    private Transform player;
    private PlayerStats playerStats;
    [SerializeField] Bucket bucket;

    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Erors at GM, why 2");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStats = player.GetComponent<PlayerStats>();
    }
    public void StartRun()
    {
        Cursor.visible = false;

        GameObject[] plants = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < plants.Length; i++)
        {
            Destroy(plants[i]);
        }

        player.GetComponent<PlayerPickUpItem>().Reset();

        player.position = startLocation.position;
        player.GetComponent<PlayerMovement>().MoonDown(false);

        //Remae the plots
        for (int i = 0; i < landPlots.Count; i++)
        {
            landPlots[i].InitPlot();
        }
        clock.StartTimer(playerStats.MoonLifeTime);
    }

    public void OpenOutGame()
    {
        outGameUi.SetActive(true);
        bucket.EmptyBucket();
    }
}
