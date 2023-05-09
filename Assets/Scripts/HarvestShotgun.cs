using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestShotgun : MonoBehaviour, IWeapon
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gunTip;
    [SerializeField] AudioSource shootSound;
    [SerializeField] Sprite image;

    [Header("Stats")]
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int pierce;
    [SerializeField] float cd;
    public float CD => cd;

    public void Activate(Vector3 mousePos)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.position = gunTip.transform.position;
            bullet.GetComponent<IBullet>().InitBullet(mousePos, damage, speed, true, pierce);
        }
        shootSound.Play();
    }

    public Sprite Image()
    {
        return image;
    }
}
