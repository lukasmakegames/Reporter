using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Slider TimerSlider;
    public GameObject panelFinish;

    public float timer = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
            return;
        timer -= Time.deltaTime;
        TimerSlider.value = timer / 10.0f;
        if (timer <= 0)
        {
            Debug.Log("Game Finish!");
            panelFinish.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
