using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    public void InitBullet(Vector3 mousePos, int gunDamage, float gunSpeed, bool player, int amountOfHits);
}
