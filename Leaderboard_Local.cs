using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard_Local : MonoBehaviour
{
    private string Country_apiURL = "http://localhost:3000/leaderboard/country";
    private string Specific_User_apiURL = "http://localhost:3000/leaderboard/user";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Country_call()
    {
        CountryFunction();
    }
    public void user_call()
    {
        UserFunction();
    }

    private IEnumerator CountryFunction()
    {
        // Create the data string in plain text format
       
        string  dataToSend = "country=Pakistan";

        // Create a UnityWebRequest to send the data
        Debug.Log("What are you sending: " + dataToSend);
        using (UnityWebRequest www = UnityWebRequest.Post(Country_apiURL, dataToSend))
        {
            // Set the content type to indicate text/plain
            www.SetRequestHeader("Content-Type", "text/plain");






            // Send the web request
            yield return www.SendWebRequest();

            // Check for errors
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data: " + www.error);
            }
            else
            {
                // Data successfully sent to the API, now process the response
                // Parse the response JSON

                string resText = www.downloadHandler.text;
                Debug.Log(resText);

                // ResponseData responseData = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                //  Debug.Log("Response: " + responseData.message);
            }
        }
    }

    private IEnumerator UserFunction()
    {
        // Create the data string in plain text format

        string dataToSend = "username=Shafeeq";

        // Create a UnityWebRequest to send the data
        Debug.Log("What are you sending: " + dataToSend);
        using (UnityWebRequest user_www = UnityWebRequest.Post(Specific_User_apiURL, dataToSend))
        {
            // Set the content type to indicate text/plain
            user_www.SetRequestHeader("Content-Type", "text/plain");






            // Send the web request
            yield return user_www.SendWebRequest();

            // Check for errors
            if (user_www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data: " + user_www.error);
            }
            else
            {
                // Data successfully sent to the API, now process the response
                // Parse the response JSON

                string resText = user_www.downloadHandler.text;
                Debug.Log(resText);

                // ResponseData responseData = JsonUtility.FromJson<ResponseData>(www.downloadHandler.text);
                //  Debug.Log("Response: " + responseData.message);
            }
        }
    }


}
public class CountryData
{
    public string message;
    // Add other fields from your JSON response here
}
public class UserData
{
    public string message;
    // Add other fields from your JSON response here
}
