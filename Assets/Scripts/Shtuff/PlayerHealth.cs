using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHp;
    private int currentHp;

    [SerializeField] Image healthBar;


    private void Start()
    {
        currentHp = maxHp;
        healthBar.fillAmount = (float)((float)currentHp / (float)maxHp);
    }
    public void TakeDamage(int amount)
    {
        currentHp -= amount;

        if(currentHp <= 0)
        {
            Die();
        }
        healthBar.fillAmount = (float)((float)currentHp / (float)maxHp);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
