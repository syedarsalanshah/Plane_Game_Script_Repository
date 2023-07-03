using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePrototype : MonoBehaviour
{
    private int Star_Score = 0;
    public Text Star_Text;

    private int Emberled_Score = 0;
    public Text Emberled_Text;

    private int Daimond_Score = 0;
    public Text Diamond_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_fun(int score)
    {
        Star_Score += score;
        Star_Text.text = Star_Score.ToString();
    }

    public void Emberled_fun(int score)
    {
        Emberled_Score += score;
        Emberled_Text.text = Emberled_Score.ToString();
    }

    public void Diamond_fun(int score)
    {
        Daimond_Score += score;
        Diamond_Text.text = Daimond_Score.ToString();
    }
}
