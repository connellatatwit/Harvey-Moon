using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IPickUpAble
{
    [SerializeField] ScoreEntity se;
    [SerializeField] float speed;
    private bool following = false;
    private Transform player;

    public ScoreEntity ScoreEntity => se;

    public void SetFollow()
    {
        following = true;
    }
    // Update is called once per frame

    private void FixedUpdate()
    {
        if (following)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public ScoreEntity GetInfo()
    {
        return se;
    }
}
