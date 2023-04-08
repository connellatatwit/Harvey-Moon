using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    [SerializeField] Transform player;
    private PlayerPickUpItem playerInv;
    [SerializeField] List<ScoreEntity> entities;

    private bool buffer = false;
    private float bufferTimer;

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
}
