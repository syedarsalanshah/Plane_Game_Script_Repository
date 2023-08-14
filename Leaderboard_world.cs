using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard_world : MonoBehaviour
{
    private string World_apiURL = "http://localhost:3000/leaderboard/world";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void clicked_function()
    {
        SendDataToAPI();
    }
    // Update is called once per frame
    private IEnumerator SendDataToAPI()
    {
      

        // Create a UnityWebRequest to send the data
        using (UnityWebRequest World_www = UnityWebRequest.Get(World_apiURL))
        {
            // Set the content type to indicate text/plain
            World_www.SetRequestHeader("Content-Type", "text/plain");






            // Send the web request
            yield return World_www.SendWebRequest();

            // Check for errors
            if (World_www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data: " + World_www.error);
            }
            else
            {
                // Data successfully sent to the API, now process the response
                // Parse the response JSON

                string resText = World_www.downloadHandler.text;
                Debug.Log(resText);

                // ResponseData responseData = JsonUtility.FromJson<ResponseData>( World_www.downloadHandler.text);
                //  Debug.Log("Response: " + responseData.message);
            }
        }
    }
}

public class WorldData
{
    public string message;
    // Add other fields from your JSON response here
}
