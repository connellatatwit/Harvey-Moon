using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject iconPrefab;
    [SerializeField] Transform InventoryImages;

    [SerializeField] GameObject outGameUi;
    [SerializeField] GameObject inGameUi;
    private Coroutine messageRoutine;

    [Header("Out Run")]
    [SerializeField] TextMeshProUGUI moneyText;

    [Header("ScoreBoard Items")]
    [SerializeField] GameObject scorePanel; // Panel that shows up in order to like, do stuff
    [SerializeField] TextMeshProUGUI newScoreText;
    private int newScore;
    [SerializeField] TextMeshProUGUI netScoreText;
    [SerializeField] TextMeshProUGUI newMoneyText;

    [SerializeField] Transform iconParent;

    [Header("Loading Screen Things")]
    [SerializeField] Animator loadingAnimator;

    [Header("Message Stuff")]
    [SerializeField] GameObject messageBox;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] float baseTextSpeed;
    [SerializeField] float ffTextSpeed;
    private float usedSpeed;
    private bool writing;
    private bool nextMessageReady;

    [Header("Planter Stuff")]
    [SerializeField] TextMeshProUGUI planterCostText;
    [SerializeField] TextMeshProUGUI amountOfSeedTypesText;

    private void Start()
    {
        usedSpeed = baseTextSpeed;
    }
    private void Update()
    {
        if (messageRoutine != null)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
            {
                if (!writing)
                {
                    StopMessage();
                }
            }
            if(Input.GetMouseButton(1) || Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.E))
            {
                usedSpeed = ffTextSpeed;
            }
            if (Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.E))
                usedSpeed = baseTextSpeed;
        }
    }
    public void InitScoreBoard(List<ScoreEntity> entities, int mult)
    {
        moneyText.gameObject.SetActive(false);

        ResetScoreBoard();
        scorePanel.SetActive(true);
        newScore = 0;
        newScoreText.text = "0";
        newMoneyText.text = "Money Earned: " + 0;
        StartCoroutine(AddItemToScore(entities, mult));

    }

    public void UpdateMoney(int newMoney)
    {
        netScoreText.text = "Money: " + newMoney;
        moneyText.text = "Money: " + newMoney.ToString();
    }

    public IEnumerator AddItemToScore(List<ScoreEntity> entities, int mult)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconParent);
            icon.GetComponent<Image>().sprite = entities[i].Image;
            newScoreText.text = "Score: " + newScore;
            newScore += entities[i].Value;
            yield return new WaitForSeconds(1f/10f);
        }
        newMoneyText.text = "Money Earned: " + (newScore* mult);
    }
    public void AddItemToInventory(GameObject item)
    {
        GameObject icon = Instantiate(iconPrefab, InventoryImages);
        icon.GetComponent<Image>().sprite = item.GetComponent<IPickUpAble>().ScoreEntity.Image;
    }
    public void RemoveItem(int pos)
    {
        Destroy(InventoryImages.GetChild(pos).gameObject);
    }

    public void ResetScoreBoard()
    {
        for (int i = iconParent.childCount - 1; i >= 0; i--)
        {
            Destroy(iconParent.GetChild(i).gameObject);
        }
    }
    public void WriteMessage(string message, string name)
    {
        if (messageRoutine != null)
            StopCoroutine(messageRoutine);
        messageRoutine = StartCoroutine(WriteMessageRoutine(message, name));
    }
    public void WriteMessage(List<string> messages, List<string> names)
    {
        StartCoroutine(WriteMessages(messages, names));
        /*for (int i = 0; i < messages.Count; i++)
        {
            while(messageRoutine != null)
            {
                Debug.Log("witing");
            }
            messageRoutine = StartCoroutine(WriteMessageRoutine(messages[i], names[i]));
        }*/
    }
    public void StopMessage()
    {
        StopCoroutine(messageRoutine);
        messageBox.SetActive(false);
        nextMessageReady = true;
    }

    public IEnumerator WriteMessageRoutine(string message, string name) // Terst TYjis
    {
        messageBox.SetActive(true);
        nameText.text = name;
        writing = true;
        nextMessageReady = false;
        for (int i = 0; i < message.Length+1; i++)
        {
            messageText.text = message.Substring(0, i);
            yield return new WaitForSeconds(usedSpeed);

        }
        writing = false;
        yield return new WaitForSeconds(10f);
        nextMessageReady = true;
        messageBox.SetActive(false);
        messageText.text = "";
    }
    public IEnumerator WriteMessageRoutine(string message, string name, float time) // Terst TYjis
    {
        usedSpeed = time;
        messageBox.SetActive(true);
        nameText.text = name;
        writing = true;
        for (int i = 0; i < message.Length + 1; i++)
        {
            messageText.text = message.Substring(0, i);
            yield return new WaitForSeconds(usedSpeed);

        }
        writing = false;
        yield return new WaitForSeconds(3f);
        messageBox.SetActive(false);
        messageText.text = "";
    }
    public IEnumerator WriteMessages(List<string> messages, List<string> names)
    {
        Debug.Log(messages.Count);
        for (int i = 0; i < messages.Count; i++)
        {
            Debug.Log(i);
            if (messageRoutine != null)
                StopCoroutine(messageRoutine);
            messageRoutine = StartCoroutine(WriteMessageRoutine(messages[i], names[i]));
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() =>  nextMessageReady == true);
            yield return new WaitForSeconds(.05f);
        }
    }

    public void CircleLoad()
    {
        loadingAnimator.SetTrigger("Circle");
    }

    public void UpdatePlanterText(Planter planter)
    {
        planterCostText.text = "$" + planter.CurrentCost.ToString();
        amountOfSeedTypesText.text = planter.GetSeeds().Count.ToString() + "/" + planter.MaxTypes.ToString();
    }
}
