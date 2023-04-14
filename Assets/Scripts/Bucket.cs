using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bucket : MonoBehaviour
{
    [SerializeField] Transform player;
    private PlayerPickUpItem playerInv;
    [SerializeField] List<ScoreEntity> entities;

    [SerializeField] TextMeshProUGUI moneyText;

    private bool buffer = false;
    private float bufferTimer;

    private int money;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerInv = player.GetComponent<PlayerPickUpItem>();
    }

    private void FixedUpdate()
    {
        if (!buffer)
        {
            if (Vector2.Distance(transform.position, player.position) <= 3f)
            {
                buffer = true;
                bufferTimer = 5f;
                for (int i = playerInv.GetInventory().Count - 1; i >= 0; i--)
                {
                    FillBucket(playerInv.GetItem(i));
                }
            }
        }
        else
        {
            bufferTimer -= Time.deltaTime;
            if (bufferTimer <= 0) buffer = false;
        }
    }

    private void FillBucket(ScoreEntity newItem)
    {
        entities.Add(newItem);
    }

    public void EmptyBucket()
    {
        int moneyTemp = 0;

        for (int i = entities.Count - 1; i >= 0; i--)
        {
            moneyTemp += entities[i].Value;
            entities.RemoveAt(i);
        }

        money += moneyTemp;

        moneyText.text = "Money: " + money.ToString();
    }

    public void SpendMoney(int anmount)
    {
        money -= anmount;
        moneyText.text = "Money: " + money.ToString();
    }

    public int Money
    {
        get { return money; }
    }
    public List<ScoreEntity> Items
    {
        get { return entities; }
    }
}
