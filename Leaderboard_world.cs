using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class PlayerData
{
    public string country;
    public string username;
    public int score;
}

[Serializable]
public class PlayerDataListWrapper
{
    public PlayerData[] players;
}

public class Leaderboard_world : MonoBehaviour
{
    public TextMeshProUGUI[] usernameTextArray;
    public TextMeshProUGUI[] CountryTextArray;
    public TextMeshProUGUI[] ScoreTextArray;
    private string World_apiURL = "http://localhost:3000/leaderboard/world";

    public void checking()
    {
        print("Hello, it is working");
    }

    public void clicked_function()
    {
        StartCoroutine(WorldSendDataToAPI());
    }


    private void Update()
    {
      /*  DateTime currentDate = DateTime.Now;
        Debug.Log("Current Date: " + currentDate.ToString("yyyy-MM-dd HH:mm:ss"));*/
    }

    private IEnumerator WorldSendDataToAPI()
    {
        using (UnityWebRequest World_www = UnityWebRequest.Get(World_apiURL))
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
                PlayerDataListWrapper playerDataListWrapper = JsonUtility.FromJson<PlayerDataListWrapper>("{\"players\":" + resText + "}");

                PlayerData[] players = playerDataListWrapper.players;



                /* for(int i=0; i<=players.Length-1; i++)
                 {
                     PlayerData player = players[i];
                     int playerIndex = i + 1;

                     print(playerIndex + " " + player.username + " " + player.country + " " + player.score);
                 }
 */
                for (int i = 0; i < players.Length; i++)
                {
                    PlayerData player = players[i];

                    if (i < usernameTextArray.Length)
                    {
                        // Update the TextMeshPro component with the username
                        usernameTextArray[i].text = $"{player.username}";
                        CountryTextArray[i].text = $"{player.country}";
                        print(player.country);
                        ScoreTextArray[i].text = $"{player.score}";
                    }
                }
            }
        }
    }
}
