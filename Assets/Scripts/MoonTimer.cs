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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerSet(maxTimer));
    }

    private IEnumerator TimerSet(float time)
    {
        yield return new WaitForSeconds(1f);
        StartTimer(time);
    }
    public void StartTimer(float time)
    {
        started = true;
        timer = time;
        maxTimer = time;
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

    private IEnumerator EndTimer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().MoonDown();
        started = false;
        bigText.gameObject.SetActive(true);
        bigText.text = "Times Up!";
        yield return new WaitForSeconds(5f);
        bigText.gameObject.SetActive(false);
    }
}
