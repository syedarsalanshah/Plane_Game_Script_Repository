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
    [SerializeField] private int minutes;
    [SerializeField] private int seconds;
    public GameObject wings_GO;
    private propeller_rotation Propeller_Script;
    private plane_Move Plane_Script;
    private Plane_Parts_Rigidbody Plane_RB_Script;
    private Fuel_Consumption Fuel_Consumption_Script;
  
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = timerDuration;
        UpdateTimerUI();
        Propeller_Script =wings_GO.GetComponent<propeller_rotation>();
        Plane_Script = GameObject.Find("biplane_main").GetComponent<plane_Move>();
        Fuel_Consumption_Script = GameObject.Find("Camera").GetComponent<Fuel_Consumption>();
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


        print(minutes + " " + seconds);
        if(minutes <= 0 &&  seconds <= 0)
        {
            print("Timer over");
        }
    }

    void CountDownTimer()
    {
        currentTimer -= Time.deltaTime;

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
         minutes = Mathf.FloorToInt(currentTimer / 60f);
         seconds = Mathf.FloorToInt(currentTimer - minutes * 60 );

        Minutes_Text.text = minutes.ToString("00"); // Display minutes with leading zero if < 10
        Seconds_Text.text = seconds.ToString("00"); // Display seconds with leading zero if < 10
    }

    void TimerEnded()
    {
        Fuel_Consumption_Script.Plane_Engine_tune.volume = 0.0f;
        Plane_Script.rotationSpeed = 0f;
        Plane_Script.tiltRotate_speed = 0f;
        Plane_Script.moveSpeed = 0f;
        Plane_Script.sideSpeed = 0f;
        ;
        Propeller_Script.Engine_ON = false;
        Propeller_Script.propeller_value = 0;
        Plane_Script.transform.Rotate(10 * Time.deltaTime, 0, 0);
        Plane_Script.gameObject.transform.Translate(Vector3.down * 20 * Time.deltaTime);
        Plane_Script.gameObject.transform.Translate(Vector3.right * 6 * Time.deltaTime);
    }
}
