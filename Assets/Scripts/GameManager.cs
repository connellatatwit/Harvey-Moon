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
    [TextArea(3,3)]
    [SerializeField] List<string> tutorialMessages;
    [SerializeField] List<string> tutorialNames;

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

    [SerializeField] List<GameObject> plants;
    [SerializeField] List<GameObject> weeds;

    private bool firstRun = true;
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

        StartCoroutine(Tutorial());
    }
    private IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(1f);
        UM.WriteMessage(tutorialMessages, tutorialNames);
    }
    public void StartRun()
    {
        if (state == GameState.OutRun)
        {
            if (planter.GetSeeds().Count <= 0)
            {
                UM.WriteMessage("I need to put seeds in the planter or nothing will grow.", "Mr. Moon");
            }
            else
            {
                state = GameState.InRun;
                Cursor.visible = false;
                UM.CircleLoad();
                StartCoroutine(ProcessRun());
                bucket.FlushMoney();
                UM.UpdateMoney(bucket.Money);
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

        // Decide Weeds
        /*
         * Spend all the planters weedvalue by making a list of weeds and subtract the weed's value from the planters value.
         * Then pick a random plot and spawn a weed inside onf of its weed spawns.
         */
        int weedMoney = planter.WeedValue * 5;
        List<GameObject> weedsToSpawn = new List<GameObject>();
        int fails = 0;
        while(weedMoney > 0)
        {
            int randWeed = Random.Range(0, planter.PossibleWeeds.Count);
            if (planter.PossibleWeeds[randWeed].GetComponent<IWeed>().Value <= weedMoney) // if the value of the weed is less than our money then spend the money
            {
                weedMoney -= planter.PossibleWeeds[randWeed].GetComponent<IWeed>().Value;
                weedsToSpawn.Add(planter.PossibleWeeds[randWeed]);
            }
            else
                fails++;
            if (fails > 10)
                break;
        }
        List<PlotOfLand> presentPlots = new List<PlotOfLand>();
        for (int i = 0; i < landPlots.Count; i++)
        {
            presentPlots.Add(landPlots[i]);
        }

        for (int i = 0; i < weedsToSpawn.Count; i++)
        {
            if (presentPlots.Count <= 0)
                break;
            int randPlot = Random.Range(0, presentPlots.Count); // Pick a plot
            presentPlots[randPlot].SpawnWeed(weedsToSpawn[i]); // SPawn the weed
            if (!presentPlots[randPlot].WeedSpace())
                presentPlots.RemoveAt(randPlot);
        }
        weedsToSpawn.Clear();

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

        //GameObject[] plants = GameObject.FindGameObjectsWithTag("Enemy");
        plants.RemoveAll(x => x == null);
        weeds.RemoveAll(x => x == null);
        for (int i = 0; i < plants.Count; i++)
        {
            Destroy(plants[i]);
        }
        for (int i = 0; i < weeds.Count; i++)
        {
            Destroy(weeds[i]);
        }
        plants.Clear();
        weeds.Clear();

        player.GetComponent<PlayerPickUpItem>().Reset();
        state = GameState.OutRun;

        //Tutorial Stuff
        if (firstRun)
        {
            EndOfFirstRun();
        }
    }
    private void EndOfFirstRun()
    {
        List<string> words = new List<string>();
        List<string> narrator = new List<string>();
        words.Add("Now that you have some money you can try to upgrade things around the farm.");
        words.Add("Everytime you start a new run your money will be reset, so make sure to try to spend it all.");
        narrator.Add("Narrator");
        narrator.Add("Narrator");
        WriteMessage(words, narrator);
        firstRun = false;
    }
    public void CheckDone()
    {
        // Checks if all plants have been harvested or left on the floor.
        plants.RemoveAll(x => x == null);
        if (plants.Count > 0)
            return;
        else
        {
            clock.EndEarly();
        }
    }

    public void AddSpawnedPlant(GameObject newPlant)
    {
        plants.Add(newPlant);
    }
    public void AddSpawnedWeed(GameObject newWeed)
    {
        weeds.Add(newWeed);
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
        UM.WriteMessage(message, name);
    }
    public void WriteMessage(List<string> messages, List<string> names)
    {
        UM.WriteMessage(messages, names);
    }
    public void UpgradePlanter()
    {
        if (CheckCost(planter.CurrentCost))
        {
            bucket.SpendMoney(planter.CurrentCost);
            planter.UpgradePlanter();
            // Update Ui
            UpdatePlanter();
        }
    }

    public bool CheckCost(int cost)
    {
        if(cost <= bucket.Money)
        {
            return true;
        }
        else
        {
            WriteMessage("Not enough Money", "Narrator");
            return false;
        }
    }
    public void UpdatePlanter()
    {
        UM.UpdatePlanterText(planter);
        UM.UpdateMoney(bucket.Money);
    }
}
