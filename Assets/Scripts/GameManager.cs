using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    InRun,
    OutRun
}

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform startLocation;
    [SerializeField] List<PlotOfLand> landPlots;
    [SerializeField] MoonTimer clock;

    [SerializeField] UiManager UM;

    private Transform player;
    private PlayerStats playerStats;
    [SerializeField] Bucket bucket;
    [SerializeField] Planter planter;

    public static GameManager instance;

    private GameState state;

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

        state = GameState.OutRun;
    }
    public void StartRun()
    {
        if (state == GameState.OutRun)
        {
            if (planter.GetSeeds().Count <= 0)
            {
                StartCoroutine(UM.WriteMessage("I need to put seeds in the planter or nothing will grow.", "Mr. Moon"));
            }
            else
            {
                state = GameState.InRun;
                Cursor.visible = false;
                UM.CircleLoad();
                StartCoroutine(ProcessRun());
            }
        }
    }

    private IEnumerator ProcessRun()
    {
        // TEMP Waits for loading to load... Sorta
        player.GetComponent<PlayerMovement>().MoonDown(true);
        yield return new WaitForSeconds(2f);
        // TEMP

        player.GetComponent<PlayerPickUpItem>().Reset();

        player.position = startLocation.position;
        player.GetComponent<PlayerMovement>().MoonDown(false);

        //Remae the plots
        for (int i = 0; i < landPlots.Count; i++)
        {
            //landPlots[i].InitPlot();
            landPlots[i].InitPlot(planter.GetSeeds(), planter.MinCrops, planter.MaxCrops);
        }
        clock.StartTimer(playerStats.MoonLifeTime);
    }

    public void ScoreBoard()
    {
        // Clear Objects on floor
        ClearPickups();

        List<ScoreEntity> temp = new List<ScoreEntity>();
        List<ScoreEntity> multiples = new List<ScoreEntity>();
        for (int i = 0; i < bucket.Items.Count; i++)
        {
            temp.Add(bucket.Items[i]);
            if (!multiples.Contains(bucket.Items[i]))
            {
                multiples.Add(bucket.Items[i]);
            }
        }
        UM.InitScoreBoard(temp, multiples.Count);
        bucket.EmptyBucket(multiples.Count);
        UM.UpdateMoney(bucket.Money);
    }

    private void ClearPickups()
    {
        GameObject[] pickUps = GameObject.FindGameObjectsWithTag("Pickup");
        for (int i = pickUps.Length - 1; i >= 0; i--)
        {
            Destroy(pickUps[i]);
        }
    }
    public void EndScoreboard()
    {
        Cursor.visible = false;
        player.GetComponent<PlayerMovement>().MoonDown(false);

        GameObject[] plants = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < plants.Length; i++)
        {
            Destroy(plants[i]);
        }

        player.GetComponent<PlayerPickUpItem>().Reset();
        state = GameState.OutRun;
    }

    public void AddPlotOfland(PlotOfLand newPlot)
    {
        landPlots.Add(newPlot);
    }

    public void UpdateUI()
    {
        UM.UpdateMoney(bucket.Money);
    }

    public int GetBucketMoney()
    {
        return bucket.Money;
    }
    public void SpendMoney(int amount)
    {
        bucket.SpendMoney(amount);
        UpdateUI();
    }

    public void WriteMessage(string message, string name)
    {
        StartCoroutine(UM.WriteMessage(message, name));
    }
    public void UpgradePlanter()
    {
        if (planter.CurrentCost <= bucket.Money)
        {
            planter.UpgradePlanter();
            // Update Ui
            UpdatePlanter();
        }
        else
            WriteMessage("Not enough Money", "Planter");
    }
    public void UpdatePlanter()
    {
        UM.UpdatePlanterText(planter);
    }
}
