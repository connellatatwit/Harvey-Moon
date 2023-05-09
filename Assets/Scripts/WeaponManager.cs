using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : CollidableObject
{
    [SerializeField] GameObject weaponUi;
    PlayerMovement player;
    private int currentWeapon = 0;
    [SerializeField] Transform weaponParent;

    [SerializeField] GameObject purchasedText;

    [SerializeField] int price;

    private bool purchased = false;

    protected override void OnCollided(GameObject hitObj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (purchased)
            {
                hitObj.GetComponent<PlayerMovement>().MoonDown(true);
                player = hitObj.GetComponent<PlayerMovement>();
            }
            OnInteract();
        }
    }

    protected virtual void OnInteract()
    {
        if (purchased)
        {
            weaponUi.SetActive(true);
            GameManager.instance.UpdatePlanter();
        }
        else
        {
            BuyWeaponCache();
        }
    }
    public void EndWeaponCache()
    {
        weaponUi.SetActive(false);
        player.MoonDown(false);
    }

    public void NextWeapon(bool up)
    {
        if (up)
        {
            currentWeapon++;
        }
        else
        {
            currentWeapon--;
        }
        currentWeapon = Mathf.Clamp(currentWeapon, 0, 2);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>().NextWeapon(currentWeapon);
    }
    public void PickWeapon(int pos)
    {
        int oldWeapon = currentWeapon;
        currentWeapon = pos;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAim>().NextWeapon(currentWeapon);

        for (int i = 0; i < weaponParent.childCount; i++)
        {
            weaponParent.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector3(425*(-(pos - i)),0,0);
        }
    }

    public void BuyWeaponCache()
    {
        if (price <= GameManager.instance.GetBucketMoney())
        {
            purchased = true;
            GameObject temp = hoverText;
            hoverText = purchasedText;
            Destroy(temp);
            GameManager.instance.SpendMoney(price);
        }
        else
        {
            GameManager.instance.WriteMessage("Not Enough Money", "Narrator");
        }
    }
}
