using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoonTimer : MonoBehaviour
{
    [SerializeField] float maxTimer;
    private float timer;

    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI bigText;

    bool started = false;
    private IEnumerator TimerSet(float time)
    {
        yield return new WaitForSeconds(1f);
        started = true;
        timer = time;
        maxTimer = time;
    }
    public void StartTimer(float time)
    {
        StartCoroutine(TimerSet(time));
    }

    private void Update()
    {
        if (started)
        {
            timer -= Time.deltaTime;
            image.fillAmount = timer / maxTimer;
            if(timer <= 0)
                StartCoroutine(EndTimer());
        }
    }
    public void EndEarly()
    {
        timer = 0;
    }

    private IEnumerator EndTimer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().MoonDown(true);
        started = false;
        bigText.gameObject.SetActive(true);
        bigText.text = "Times Up!";
        yield return new WaitForSeconds(3.5f);
        bigText.gameObject.SetActive(false);

        GameManager.instance.ScoreBoard();
        Cursor.visible = true;
    }
}
