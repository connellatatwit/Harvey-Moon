using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponCoatHanger : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] int posOnRack;
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject WeaponCache = GameObject.FindGameObjectWithTag("Weapon Cache");
        WeaponCache.GetComponent<WeaponManager>().PickWeapon(posOnRack);
    }

}
