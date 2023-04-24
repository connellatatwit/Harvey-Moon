using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SeedPacket : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IPointerDownHandler
{
    [Header("Set Up")]
    [SerializeField] GameObject seedPrefab;

    [Header("Unity Stuff")]
    [SerializeField] Canvas canvas; // This likely will need to be changed... But maybe not >.>

    [SerializeField] GameObject lockedImage;

    [SerializeField] GameObject nameObject;
    [SerializeField] Sprite packetSprite;
    [SerializeField] Sprite seedSprite;

    [Header("Purchase Stuff")]
    [SerializeField] bool locked = true;
    [SerializeField] int cost;
    [SerializeField] GameObject purchaseButtons;

    private Image image;
    private RectTransform rectTransform;
    private Vector2 startPos;
    Transform parent;

    private bool equipped = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = rectTransform.anchoredPosition;
        parent = transform.parent;
        transform.SetParent(transform.parent.parent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!locked)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

            if (!equipped)
            {
                if (rectTransform.anchoredPosition.y >= 0)
                {
                    image.sprite = seedSprite;
                }
                else
                {
                    image.sprite = packetSprite;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!equipped)
        {
            if (rectTransform.anchoredPosition.y >= 0)
            {
                //Find Planter and do planter stuff
                Planter planter = GameObject.FindGameObjectWithTag("Planter").GetComponent<Planter>();
                if (!planter.AddSeed(seedPrefab, transform))
                {
                    ResetPacket();
                }
                else
                    equipped = true;
            }
            else
            {
                ResetPacket();
            }
        }
        else
        {
            equipped = false;
            Planter planter = GameObject.FindGameObjectWithTag("Planter").GetComponent<Planter>();
            planter.RemoveSeed(seedPrefab, transform);
            image.sprite = packetSprite;
        }
    }

    private void ResetPacket()
    {
        rectTransform.anchoredPosition = startPos / canvas.scaleFactor;
        if (equipped)
            image.sprite = seedSprite;
        else
            image.sprite = packetSprite;
        transform.SetParent(parent);
    }

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = packetSprite;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Unlock()
    {
        locked = false;
        lockedImage.SetActive(false);
        GameManager.instance.SpendMoney(cost);
        nameObject.SetActive(true);
        purchaseButtons.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (locked)
        {
            if (GameManager.instance.GetBucketMoney() >= cost)
                purchaseButtons.SetActive(true);
        }
    }
}
