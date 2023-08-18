using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class CountryData
{
    public string score;
    public string username;
}

[Serializable]
public class CountryDataListWrapper
{
    public CountryData[] Country_players;
}

public class Leaderboard_Local : MonoBehaviour
{
    public TextMeshProUGUI[] Local_usernameTextArray;
    public TextMeshProUGUI[] Local_ScoreTextArray;
    public TMP_Dropdown dropdown_Leaderboard;

    private List<string> countries = new List<string>
    {
        "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Antigua Barbuda", "Argentina", "Armenia", "Australia", "Austria",
        "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bhutan",
        "Bolivia", "Bosnia and Herzegovina", "Botswana", "Brazil", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon",
        "Canada", "Cape Verde", "Central African Republic", "Chad", "Chile", "China", "Colombia", "Comoros",  "Congo, Republic of the",
        "Costa Rica", "Côte d'Ivoire", "Croatia", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic",
        "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Fiji", "Finland",
        "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Greece", "Grenada", "Guatemala", "Guinea",
        "Guinea-Bissau", "Guyana", "Haiti", "Honduras", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq",
        "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kosovo",
        "Kuwait", "Kyrgyzstan", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania",
        "Luxembourg", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Mauritania",
        "Mauritius", "Mexico", "Micronesia, Federated States of", "Moldova", "Monaco", "Mongolia", "Montenegro", "Morocco", "Mozambique",
       "Myanmar (Burma)", "Namibia", "Nauru", "Nepal", "Netherlands", "New Zealand", "Nicaragua", "Niger", "Nigeria", "North Korea",
       "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland",
       "Portugal", "Qatar", "Romania", "Russia", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa",
       "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia",
       "Solomon Islands", "Somalia", "South Africa", "South Korea", "South Sudan", "Spain", "Sri Lanka", "Sudan", "Suriname", "Swaziland",
       "Sweden", "Switzerland", "Syria", "Tajikistan", "Tanzania", "Thailand", "Timor-Leste", "Togo", "Tonga", "Trinidad and Tobago",
       "Tunisia", "Turkey", "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "UK", "USA",
       "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe"
    };

    private string Country_name;
    private string Country_apiURL = "http://localhost:3000/leaderboard/country";

    // Start is called before the first frame update
    void Start()
    {
        dropdown_Leaderboard.AddOptions(countries);
        InvokeRepeating("Leaderboard_Call", 1, 2);
    }

    private void Leaderboard_Call()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(CountryFunction());
        }
        else
        {
            Debug.Log("Now sleeping");
        }
    }

    public void Country_call()
    {
        StartCoroutine(CountryFunction());
    }

    // ... Other methods ...

    private IEnumerator CountryFunction()
    {
        // Create the data string in plain text format
        Country_name = dropdown_Leaderboard.options[dropdown_Leaderboard.value].text;
        string dataToSend = "country=" + Country_name;

        // Create a UnityWebRequest to send the data
        using (UnityWebRequest www = UnityWebRequest.Post(Country_apiURL, dataToSend))
        {
            www.SetRequestHeader("Content-Type", "text/plain");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error sending data: " + www.error);
            }
            else
            {
                string resText = www.downloadHandler.text;
                Debug.Log(resText);

                CountryDataListWrapper Country_playerDataListWrapper = JsonUtility.FromJson<CountryDataListWrapper>("{\"Country_players\":" + resText + "}");

                CountryData[] Country_players = Country_playerDataListWrapper.Country_players;

                for (int i = 0; i < Country_players.Length && i < Local_usernameTextArray.Length; i++)
                {
                    Local_usernameTextArray[i].text = $"{Country_players[i].username}";
                    Local_ScoreTextArray[i].text = $"{Country_players[i].score}";
                }
            }
        }
    }
}
