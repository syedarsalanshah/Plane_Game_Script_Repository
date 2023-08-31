using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Sending_Game_Data_Script : MonoBehaviour
{
    public Network_Script Networking_Script;
    public GameObject Plane_GO;
    private plane_Move Plane_Script;
    private string UserApi = "http://localhost:3000/user/highscore";
    // Start is called before the first frame update

    public GameObject Timer_GO;
    private Timer_Script Time_Script;
    void Start()
    {
        Plane_Script = Plane_GO.GetComponent<plane_Move>();
       
        Time_Script = Timer_GO.GetComponent <Timer_Script>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CalltoSendDataFun()
    {
        StartCoroutine(SendingUserData());
    }

    private IEnumerator SendingUserData()
    {



        DateTime currentDateTime = DateTime.Now;
        string CDateTime =  currentDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        string dataToSend = "username="+ Network_Script.Caretaker_of_Player + "&score="+Plane_Script.current_Score+"&DateTime="+CDateTime+ "&Level=1" + "&Timer =0" + Time_Script.minutes + ":" + Time_Script.seconds;


        using (UnityWebRequest www = UnityWebRequest.Post(UserApi, dataToSend))
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


              //  ResponseData responseData = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                //Debug.Log("Response: " + responseData.message);


            }
        }
    }
}
