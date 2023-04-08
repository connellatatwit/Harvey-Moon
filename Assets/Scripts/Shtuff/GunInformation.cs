using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunInformation
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float fireCd;

    [SerializeField] float shakeStrength;

    [SerializeField] bool rapidFire;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Sprite gunImage;

    public int GetDamage()
    {
        Debug.Log("Damage is: " + damage);
        return damage;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public GameObject GetBullet()
    {
        return bulletPrefab;
    }
    public float GetShakeStrength()
    {
        return shakeStrength;
    }
    public bool RapidFire()
    {
        return rapidFire;
    }
    public float FireCd()
    {
        return fireCd;
    }
    public Sprite GetImage()
    {
        return gunImage;
    }
    public void AddAtkSpeed(float amount)
    {
        fireCd = (1-amount) * fireCd;
    }


    public static GunInformation DeepCopy(GunInformation newGun)
    {
        GunInformation temp = new GunInformation();

        temp.damage = newGun.damage;
        temp.speed = newGun.speed;
        temp.bulletPrefab = newGun.bulletPrefab;
        temp.shakeStrength = newGun.shakeStrength;
        temp.rapidFire = newGun.rapidFire;
        temp.fireCd = newGun.fireCd;
        temp.gunImage = newGun.gunImage;

        return temp;
    }
}
