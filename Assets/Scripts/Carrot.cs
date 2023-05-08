using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour, Enemy
{
    [SerializeField] GameObject droppedCarrotPrefab;
    [SerializeField] int maxHealth;
    private int currentHealth;
    [SerializeField] List<GameObject> possibleWeeds;

    public GameObject Drop
    {
        get { return droppedCarrotPrefab; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public List<GameObject> PossibleWeeds
    {
        get { return possibleWeeds; }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Instantiate(droppedCarrotPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
