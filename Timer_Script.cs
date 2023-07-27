using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Script : MonoBehaviour
{
    public Text Minutes_Text;
    public Text Seconds_Text;
    private float timerDuration = 300f; // 5 minutes in seconds
    private float currentTimer;
    private bool isTimerActive = false;

    public GameObject wings_GO;
    private propeller_rotation Propeller_Script;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = timerDuration;
        UpdateTimerUI();
        Propeller_Script =wings_GO.GetComponent<propeller_rotation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Propeller_Script.Engine_ON == true)
        {
            isTimerActive = true;
        }
        else
        {
            isTimerActive = false;
        }

        if (isTimerActive && currentTimer > 0f)
        {
            CountDownTimer();
        }
        else if (currentTimer <= 0f)
        {
            print("Game Over");
        }
    }

    void CountDownTimer()
    {
        currentTimer -= Time.deltaTime;

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTimer / 60f);
        int seconds = Mathf.FloorToInt(currentTimer - minutes * 60);

        Minutes_Text.text = minutes.ToString("00"); // Display minutes with leading zero if < 10
        Seconds_Text.text = seconds.ToString("00"); // Display seconds with leading zero if < 10
    }
}
