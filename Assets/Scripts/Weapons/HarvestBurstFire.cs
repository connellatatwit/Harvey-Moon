using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestBurstFire : MonoBehaviour, IWeapon
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
        StartCoroutine(Burst(mousePos));
    }
    public IEnumerator Burst(Vector3 pos)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.position = gunTip.transform.position;
            bullet.GetComponent<IBullet>().InitBullet(pos, damage, speed, true, pierce);
            shootSound.Play();
            yield return new WaitForSeconds(.075f);
        }
    }

    public Sprite Image()
    {
        return image;
    }
}
