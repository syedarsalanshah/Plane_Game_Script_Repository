using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Checking_Script : MonoBehaviour
{
    public GameObject slider_val;
    public bool Checking = true;
    private float value = 1;
    public float test_value = 33;
    public float calling_time;
    // Start is called before the first frame update
    void Start()
    {
        calling_time = Time.time;
        InvokeRepeating("inc", 1.0f, 1.0f);

        
        InvokeRepeating("dec", 1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {


   
    }

    private void FixedUpdate()
    {
      
    }
    void inc()
    {
        if(Checking == true)
        {
            slider_val.LeanScaleX(1, 1);
            StartCoroutine(Finish_Fuel_Ful_Timer());
        }
    }
    IEnumerator Finish_Fuel_Ful_Timer()
    {
        yield return new WaitForSeconds(1f);
        Checking = false;
    }
    void dec()
    {
        if (!(slider_val.gameObject.transform.localScale.x <= 0))
        {


            if (Checking == false)
            {
                slider_val.LeanScaleX(slider_val.gameObject.transform.localScale.x - 0.1f, 3);

                if (slider_val.gameObject.transform.localScale.x <= 0)
                {
                    slider_val.LeanScaleX(0, 1);
                }
            }
        }
        

       /* test_value = test_value >= 0.1f ? test_value-0.1f : 0;
        if(test_value >= 0)
        {
            slider_val.LeanScaleX(test_value, 1);
        }
        else
        {
            slider_val.LeanScaleX(0, 1);
        }*/
        
    }
}
