using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


[Serializable]
public class TourGlobalPlayerData
{
    public string TourGlobalcountry;
    public string TourGlobalusername;
    public int TourGlobalscore;
}

[Serializable]
public class TourGlobalPlayerDataListWrapper
{
    public TourGlobalPlayerData[] TourGlobalplayers;
}
public class Tournament_Global : MonoBehaviour





{
    public TextMeshProUGUI[] TourGlobalusernameTextArray;
    public TextMeshProUGUI[] TourGlobalCountryTextArray;
    public TextMeshProUGUI[] TourGlobaScoreTextArray;
    private string World_apiURL = "http://localhost:3000/tournamentleaderboard";

   

    public void TourGlobalclicked_function()
    {
        StartCoroutine(TourGlobaWorldSendDataToAPI());
    }


    private void Update()
    {
        /*  DateTime currentDate = DateTime.Now;
          Debug.Log("Current Date: " + currentDate.ToString("yyyy-MM-dd HH:mm:ss"));*/
    }

    private IEnumerator TourGlobaWorldSendDataToAPI()
    {
        string datatosend = "Tournament_id="/*global_tournament_id*/;
        using (UnityWebRequest World_www = UnityWebRequest.Post(World_apiURL,datatosend))
        {
            World_www.SetRequestHeader("Content-Type", "application/json");

            yield return World_www.SendWebRequest();

            if (World_www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data: " + World_www.error);
            }
            else
            {
                string resText = World_www.downloadHandler.text;
                Debug.Log(resText);

                // Deserialize the JSON response
                TourGlobalPlayerDataListWrapper TourGlobalplayerDataListWrapper = JsonUtility.FromJson<TourGlobalPlayerDataListWrapper>("{\"players\":" + resText + "}");

                TourGlobalPlayerData[] TourGlobalplayers = TourGlobalplayerDataListWrapper.TourGlobalplayers;



                /* for(int i=0; i<=players.Length-1; i++)
                 {
                     PlayerData player = players[i];
                     int playerIndex = i + 1;

                     print(playerIndex + " " + player.username + " " + player.country + " " + player.score);
                 }
 */
                for (int i = 0; i < TourGlobalplayers.Length; i++)
                {
                    TourGlobalPlayerData TourGlobalplayer = TourGlobalplayers[i];

                    if (i < TourGlobalusernameTextArray.Length)
                    {
                        // Update the TextMeshPro component with the username
                        TourGlobalusernameTextArray[i].text = $"{TourGlobalplayer.TourGlobalusername}";
                        TourGlobalCountryTextArray[i].text = $"{TourGlobalplayer.TourGlobalcountry}";
                        print(TourGlobalplayer.TourGlobalcountry);
                        TourGlobaScoreTextArray[i].text = $"{TourGlobalplayer.TourGlobalscore}";
                    }
                }
            }
        }
    }
}
