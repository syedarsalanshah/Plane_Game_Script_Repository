using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Screen_UI_Script : MonoBehaviour
{
    public GameObject Pause_Menu;
    public GameObject Winner_Menu;
    public GameObject Loser_Menu;

    public GameObject Text_as_Goal_GO;
    public GameObject Image_as_Goal_GO;


    public GameObject LeaderboardBar;
    public GameObject Circle1;
    public GameObject Circle2;
    public GameObject Circle3;
    public Text CurrentScore_UI_Text;
    public Text HighestScore_UI_Text;
    public Text Loser_Current_UI_Text;

    public GameObject Fuel_Script_gameobject;
    private Fuel_Consumption Fuel_Script;
    public GameObject Main_Plane_Object;
    private plane_Move Plane_Script;
    private bool StartEngine_Indicator_BOOL;
    public propeller_rotation Propeller_Script;
    // Start is called before the first frame update
    void Start()

    {
     //   LeaderboardBar.SetActive(false);

        Pause_Menu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;

        Plane_Script = Main_Plane_Object.GetComponent<plane_Move>();
        Fuel_Script = Fuel_Script_gameobject.GetComponent<Fuel_Consumption>();
        InvokeRepeating("StartEnigine_ON", 0, 1);
        InvokeRepeating("Hide_Reach_Goal", 0, 0.2f);
    }

    void Hide_Reach_Goal()
    {
        if(Propeller_Script.Engine_ON == true)
        {
            Text_as_Goal_GO.SetActive(false);
            Image_as_Goal_GO.SetActive(false);
            CancelInvoke("Hide_Reach_Goal");
        }
    }
    void StartEnigine_ON()
    {
        if (Propeller_Script.Engine_ON==false)
        {
            Circle1.gameObject.SetActive(false);
            Circle2.gameObject.SetActive(true);
            Circle3.gameObject.SetActive(true);
            StartEngine_Indicator_BOOL = false;
            StartCoroutine(ON_OFF());
           
        }

        if (Propeller_Script.Engine_ON == true)
        {
            Circle1.gameObject.SetActive(false);
            Circle2.gameObject.SetActive(false);
            Circle3.gameObject.SetActive(false);

            CancelInvoke("StartEnigine_ON");

        }
    }

    IEnumerator ON_OFF()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Circle1.gameObject.SetActive(true);
        Circle2.gameObject.SetActive(false);
        Circle3.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !Winner_Menu.gameObject.activeSelf && !Loser_Menu.gameObject.activeSelf)
        {
            Pause_menu();
        }

        if (Pause_Menu.gameObject.activeSelf)
        {
            Fuel_Script.Plane_Engine_tune.Pause();
        }

        if (Pause_Menu.gameObject.activeSelf || Winner_Menu.gameObject.activeSelf || Loser_Menu.gameObject.activeSelf)
        {
            Fuel_Script.fuel_alaram_tune.Pause();
            Fuel_Script.Crash_tune.Pause();
            Fuel_Script.Petrol_Filling_tune.Pause();
            Fuel_Script.Plane_Engine_tune.Pause();
            Fuel_Script.Plane_Blast.Pause();
        }



    }

   
  

    public void Pause_menu()
    {
        

        Pause_Menu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void Resume_menu()
    {
        Fuel_Script.fuel_alaram_tune.UnPause();
        Fuel_Script.Crash_tune.UnPause();
        Fuel_Script.Plane_Engine_tune.UnPause();
        Fuel_Script.Plane_Blast.UnPause();
        Fuel_Script.Plane_Engine_tune.Play();
        Time.timeScale = 1.0f;
        Pause_Menu.gameObject.SetActive(false);

    }
    public void GotoMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Winner_Menu_Function()
    {
       
        Time.timeScale = 0.0f;
        Winner_Menu.gameObject.SetActive(true);

        CurrentScore_UI_Text.text = Plane_Script.current_Score.ToString();
        HighestScore_UI_Text.text = Plane_Script.Best_Total_Score.ToString();
    }

    public void Loser_Menu_Function()
    {
        
        Time.timeScale = 0.0f;
        Loser_Menu.gameObject.SetActive(true);

        Loser_Current_UI_Text.text = Plane_Script.current_Score.ToString();
       
    }

    public void Restart_Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenGlobalLeaderboard()
    {
        LeaderboardBar.SetActive(true);
        Winner_Menu.SetActive(false);
    }

    public void Back_toWinningMenu()
    {
        LeaderboardBar.SetActive(false);
        Winner_Menu.SetActive(true);
    }

}
