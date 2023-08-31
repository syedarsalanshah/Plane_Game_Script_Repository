using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Tournament_Registration : MonoBehaviour
{
    public GameObject StayTunedMessage_GO;
    public GameObject CongratsLocalMessage_GO;
    public GameObject CongratsGlobalMessage_GO;
    public GameObject CurrentlyLocalRegisMessage_GO;
    public GameObject CurrentlyGlobalRegisMessage_GO;


    private bool MessageAlert;
    public static string GlobalTourID;
    public static string LocalTourID;
    private string nonLocalTourID;
    private string nonGlobalTourID;
    private string http_Tour_Details = "http://localhost:3000/tournament/tournamentLinks";
    private string detailForTour;
    [SerializeField] private GameObject RegistrationPanel;
    [SerializeField] private GameObject TourPanel;
    // Start is called before the first frame update
    void Start()
    {
        Network_Script.Caretaker_of_Player = PlayerPrefs.GetString("UserIDPlayer_Pref", "");
        LocalTourID = PlayerPrefs.GetString("LocalTourIDPlayer_Pref", "");
        GlobalTourID = PlayerPrefs.GetString("GlobalTourIDPlayer_Pref", "");
    }

    // Update is called once per frame
    void Update()
    {
        print(Network_Script.Caretaker_of_Player);
    }
    public void AllowingtoTour()
    {
        if(Network_Script.Caretaker_of_Player == "No")
        {
            RegistrationPanel.SetActive(true);
        }
        else
        {
            TourPanel.SetActive(true);
            StartCoroutine(GetTourDetails());
        }


    }

    public void LocalCLickHereButtonfun()
    {
        /* 
         if(MessageAlert == true){
           StayTuned_GO.setActive(true);
        startcoroutine(TurnOff());

         }
        else{
         if(nonLocalTourID == LocalTourID)
         {
             CurrentlyLocalRegisMessage_GO.SetActive(true);
             startcoroutine(TurnOff());
         }
         else
         {
             LocalTourID = nonLocalTourID;
             PlayerPrefs.SetString("LocalTourIDPlayer_Pref", LocalTourID);
             CongratsLocalMessage_GO.SetActive(true);
             startcoroutine(TurnOff());

         }
        }*/
    }

    public void GlobalCLickHereButtonfun()
    {
        /*if (nonGlobalTourID == GlobalTourID)
        {
              CurrentlyGlobalRegisMessage_GO.SetActive(true);
             startcoroutine(TurnOff());
        }
        else
        {
            GlobalTourID = nonGlobalTourID;
            PlayerPrefs.SetString("GlobalTourIDPlayer_Pref", GlobalTourID);
            CongratsGlobalMessage_GO.SetActive(true);
             startcoroutine(TurnOff());

        }*/
    }

    IEnumerator TurnOff()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        if (StayTunedMessage_GO.activeSelf == true)
        {
            StayTunedMessage_GO.SetActive(false);
        }

        if (CongratsLocalMessage_GO.activeSelf == true)
        {
            CongratsLocalMessage_GO.SetActive(false);
        }

        if (CongratsGlobalMessage_GO.activeSelf == true)
        {
            CongratsGlobalMessage_GO.SetActive(false);
        }

        if (CurrentlyLocalRegisMessage_GO.activeSelf == true)
        {
            CurrentlyLocalRegisMessage_GO.SetActive(false);
        }

        if (CurrentlyGlobalRegisMessage_GO.activeSelf == true)
        {
            CurrentlyGlobalRegisMessage_GO.SetActive(false);
        }
        
    }

    IEnumerator GetTourDetails()
    {
        //detailForTour = userid + Level =1
        using (UnityWebRequest www = UnityWebRequest.Post(http_Tour_Details,detailForTour))
        {
            // Set the content type to indicate text/plain
            www.SetRequestHeader("Content-Type", "text/plain");






            // Send the web request
            yield return www.SendWebRequest();

            // Check for errors
            if (www.result != UnityWebRequest.Result.Success)
            {
                // StartCoroutine(SendingUserData());
                Debug.LogError("Error sending data: " + www.error);
            }
            else
            {
                // Data successfully sent to the API, now process the response
                // Parse the response JSON

                string resText = www.downloadHandler.text;


                ResponseTourData responseData = JsonUtility.FromJson<ResponseTourData>(www.downloadHandler.text);
                Debug.Log("Response: " + responseData.messagetour);
                if (responseData.messagetour == "Not currently")
                {
                   // MessageAlert = true;
                }
                else
                {
                    //nonLocalTourID = Local Messge Tour ID;
                    //nonGlobalTourID = Global Messge Tour ID;
                    //MessageAlert = false;
                }


            }
        }
    }
    public class ResponseTourData
    {
        public string messagetour;
        // Add other fields from your JSON response here
    }
}

