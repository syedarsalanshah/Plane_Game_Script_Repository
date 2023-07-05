using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Optons_Volumn : MonoBehaviour
{
    public Slider volume_Slider;
    public AudioSource Background_Music;

    // Start is called before the first frame update
    void Start()
    {
        volume_Slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
       Background_Music.volume = volume_Slider.value;
    }
}
