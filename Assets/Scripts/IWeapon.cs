using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Activate(Vector3 mousePos);
    public Sprite Image();
    public float CD { get; }
}
