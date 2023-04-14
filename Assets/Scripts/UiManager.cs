using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject iconPrefab;
    [SerializeField] Transform InventoryImages;

    [SerializeField] GameObject outGameUi;

    [Header("ScoreBoard Items")]
    [SerializeField] GameObject scorePanel; // Panel that shows up in order to like, do stuff
    [SerializeField] TextMeshProUGUI newScoreText;
    private int newScore;
    [SerializeField] TextMeshProUGUI netScoreText;

    [SerializeField] Transform iconParent;

    [Header("Loading Screen Things")]
    [SerializeField] Animator loadingAnimator;


    public void InitScoreBoard(List<ScoreEntity> entities)
    {
        ResetScoreBoard();
        scorePanel.SetActive(true);
        newScore = 0;
        newScoreText.text = "0";
        StartCoroutine(AddItemToScore(entities));

    }

    public void InitOutGame()
    {
        outGameUi.SetActive(true);
    }

    public void UpdateMoney(int newMoney)
    {
        netScoreText.text = "Money: " + newMoney;
    }

    public IEnumerator AddItemToScore(List<ScoreEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconParent);
            icon.GetComponent<Image>().sprite = entities[i].Image;
            newScoreText.text = "Score: " + newScore;
            newScore += entities[i].Value;
            yield return new WaitForSeconds(1f/10f);
        }
    }
    public void AddItemToInventory(GameObject item)
    {
        GameObject icon = Instantiate(iconPrefab, InventoryImages);
        icon.GetComponent<Image>().sprite = item.GetComponent<IPickUpAble>().ScoreEntity.Image;
    }
    public void RemoveItem(int pos)
    {
        Destroy(InventoryImages.GetChild(pos).gameObject);
    }

    public void ResetScoreBoard()
    {
        for (int i = iconParent.childCount - 1; i >= 0; i--)
        {
            Destroy(iconParent.GetChild(i).gameObject);
        }
    }

    public void CircleLoad()
    {
        loadingAnimator.SetTrigger("Circle");
    }
}
