using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Correction : MonoBehaviour
{
    public float button_threshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

        this.GetComponent<Image>().alphaHitTestMinimumThreshold = button_threshold;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
