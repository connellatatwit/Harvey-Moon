using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponCoatHanger : MonoBehaviour, IPointerDownHandler//, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int posOnRack;
    [SerializeField] bool owned;
    [SerializeField] GameObject textObject;
    [SerializeField] GameObject lockedObject;
    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject WeaponCache = GameObject.FindGameObjectWithTag("Weapon Cache");
        if (owned)
        {
            WeaponCache.GetComponent<WeaponManager>().PickWeapon(posOnRack);
        }
        else
        {
            WeaponCache.GetComponent<WeaponManager>().MoveWeapon(posOnRack);
        }
    }
    public void SetPos(int newPos)
    {
        posOnRack = newPos;
    }
    public void SetOwned(bool owned)
    {
        this.owned = owned;
        if(textObject != null && owned)
        {
            Destroy(textObject);
        }
        if(lockedObject != null)
            lockedObject.SetActive(!owned);
    }

/*    public void OnPointerEnter(PointerEventData eventData)
    {
        if(textObject!= null)
        {
            textObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
        {
            if(textObject != null)
            {
                textObject.SetActive(false);
            }
        }
    }*/
}
