using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterHelper : MonoBehaviour
{
    public void FireGun()
    {
        transform.parent.GetComponent<GunEnemy>().ShootAtPlayer();
    }
    public void DoneShooting()
    {
        transform.parent.GetComponent<GunEnemy>().DoneShoting();
    }
    public void Dodge()
    {
        transform.parent.GetComponent<GunEnemy>().Dodge();

    }
    public void Walk()
    {
        transform.parent.GetComponent<GunEnemy>().Walk();

    }
    public void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
