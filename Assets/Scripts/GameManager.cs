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
            state = GameState.InRun;
            Cursor.visible = false;
            UM.CircleLoad();
            StartCoroutine(ProcessRun());
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
            landPlots[i].InitPlot();
        }
        clock.StartTimer(playerStats.MoonLifeTime);
    }

    public void OpenOutGame()
    {
        UM.InitOutGame();
    }

    public void ScoreBoard()
    {
        List<ScoreEntity> temp = new List<ScoreEntity>();
        for (int i = 0; i < bucket.Items.Count; i++)
        {
            temp.Add(bucket.Items[i]);
        }
        UM.InitScoreBoard(temp);
        bucket.EmptyBucket();
        UM.UpdateMoney(bucket.Money);
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
}
