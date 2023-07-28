using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class Network_Script : MonoBehaviour
{


    public TMP_Dropdown dropdown;
    public TMP_InputField uname;
    public TMP_InputField userid;
    public TMP_InputField email;
    private string selectedValue;
    private string uname_string;
    private string userid_string;
    private string email_string;
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
       "Tunisia", "Turkey", "Turkmenistan", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "United States of America",
       "Uruguay", "Uzbekistan", "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Yemen", "Zambia", "Zimbabwe"
    };


    private string apiURL = "http://localhost:3000/createaccount";

    // Sample data (replace with your actual data)
  //  private string username = "Sir Abid";
  //  private string email = "Sirabid@gmail.com";

    void Start()
    {
        // ... (same as before)

      
        dropdown.AddOptions(countries);
    }

    private void Update()
    {
        selectedValue = dropdown.options[dropdown.value].text;
        uname_string = uname.text;
        userid_string = userid.text;
        email_string = email.text;

        
    }

    public void Ok_button()
    {
        StartCoroutine(SendDataToAPI());
    }

    private IEnumerator SendDataToAPI()
    {
        // Create the data string in plain text format
        string dataToSend = "username=" + userid_string +"&name=" + uname_string + "&email=" + email_string + "&country=" + selectedValue ;

        // Create a UnityWebRequest to send the data
        using (UnityWebRequest www = UnityWebRequest.Post(apiURL, dataToSend))
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
                string responseText = www.downloadHandler.text;
                Dictionary<string, object> responseData = JsonUtility.FromJson<Dictionary<string, object>>(responseText);

                // Check if the request was accepted in the response
                if (responseData.ContainsKey("status") && responseData["status"].ToString() == "success")
                {
                    Debug.Log("Data sent successfully! Request accepted.");
                }
                else
                {
                    Debug.Log("Data sent successfully, but request was not accepted.");
                }
            }
        }
    }
}
