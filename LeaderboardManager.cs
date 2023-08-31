using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject RegistrationPanel;
    public GameObject Global_Leaderboard;

    public GameObject Global_Player_GO;
    private Leaderboard_world WorldLeaderboard_Script;
    // Start is called before the first frame update
    void Start()
    {
        WorldLeaderboard_Script = Global_Player_GO.GetComponent<Leaderboard_world>();
    }
    public void Verifiying()
    {
        if(Network_Script.Caretaker_of_Player == "No")
        {
            RegistrationPanel.SetActive(true);
        }
        else
        {
            Global_Leaderboard.SetActive(false);
            WorldLeaderboard_Script.clicked_function();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
