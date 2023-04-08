using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject iconPrefab;
    [SerializeField] Transform InventoryImages;

    public void AddItemToInventory(GameObject item)
    {
        GameObject icon = Instantiate(iconPrefab, InventoryImages);
        icon.GetComponent<Image>().sprite = item.GetComponent<IPickUpAble>().ScoreEntity.Image;
    }
    public void RemoveItem(int pos)
    {
        Destroy(InventoryImages.GetChild(pos).gameObject);
    }
}
