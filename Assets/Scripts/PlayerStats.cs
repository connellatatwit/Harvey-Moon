using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] float baseMoonLifeTime;
    private float moonLifeTime;
    [SerializeField] float baseSpeed;
    private float speed;

    public float MoonLifeTime
    {
        get { return moonLifeTime; }
    }
    public float Speed
    {
        get { return speed; }
    }

    public void UpgradeStat(Stats targetStat, int amount)
    {
        if(targetStat == Stats.Speed)
        {
            speed += amount;
        }
        else if(targetStat == Stats.Moonlife)
        {
            moonLifeTime += amount;
        }
    }

    private void Start()
    {
        InitPlayer();
    }
    public void InitPlayer()
    {
        speed = baseSpeed;
        moonLifeTime = baseMoonLifeTime;
    }
}
