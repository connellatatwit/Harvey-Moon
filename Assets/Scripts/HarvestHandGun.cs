using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestHandGun : MonoBehaviour, IWeapon
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

    //private CameraControl cam;

    public void Activate(Vector3 mousePos)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.position = gunTip.transform.position;
        bullet.GetComponent<Bullet>().InitBullet(mousePos, damage, speed, true, pierce);

        shootSound.Play();
        //cam.Shake((transform.position - gunTip.position).normalized, currentGunInfo.GetShakeStrength(), .05f);
    }
    public Sprite Image()
    {
        return image;
    }
}
