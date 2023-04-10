using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpItem : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] UiManager UIM;

    [SerializeField] List<ScoreEntity> heldItems;

    [SerializeField] LayerMask layer;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, range, layer);
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetComponent<IPickUpAble>() != null)
            {
                items[i].GetComponent<IPickUpAble>().SetFollow();
            }
        }
    }

    public void AddToInventory(GameObject item)
    {
        UIM.AddItemToInventory(item);
        heldItems.Add(item.GetComponent<IPickUpAble>().ScoreEntity);
        Destroy(item);
    }
    public List<ScoreEntity> GetInventory()
    {
        return heldItems;
    }
    public ScoreEntity GetItem(int pos)
    {
        ScoreEntity temp = heldItems[pos];
        heldItems.RemoveAt(pos);
        UIM.RemoveItem(pos);
        return temp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPickUpAble>() != null)
        {
            AddToInventory(collision.gameObject);
        }
    }

    public void Reset()
    {
        for (int i = heldItems.Count - 1; i >= 0; i--)
        {
            heldItems.RemoveAt(i);
            UIM.RemoveItem(i);
        }
    }
}
