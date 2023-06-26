using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using JetBrains.Annotations;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Fuel_Consumption : MonoBehaviour
{
    
    public GameObject bar;
    public int Timer;
    public Image barr;
    public int val;
    public int value_of_fuel_zero = 1;
    public bool bool_of_fuel_zero = false;
    public GameObject Plane_as_Object;
    private plane_Move Planes_Script;


    public AudioSource fuel_alaram_tune;
    public AudioSource Crash_tune;
    private int counter_for_fuel_alaram = 1;


    private int decline_of_plane_counter = 1;
    // Start is called before the first frame update
    void Start()
    {
      //  AnimateBar();
        Planes_Script = Plane_as_Object.GetComponent<plane_Move>();
       
       bar_decrease_sequence();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (barr.rectTransform.localScale.x == 1.0f)
        {
            bar_decrease_sequence();

        }


        if(barr.rectTransform.localScale.x <= 0.5f && barr.rectTransform.localScale.x >= 0.01f)
        {

            if(counter_for_fuel_alaram == 1)
             {
                
                 fuel_alaram_tune.Play();
                counter_for_fuel_alaram = 0;



             }
        }
        else
        {
            fuel_alaram_tune.Stop();
            counter_for_fuel_alaram = 1;
            
        }


        if (barr.rectTransform.localScale.x <= 0.35f && barr.rectTransform.localScale.x >= 0.001f)
        {
            if (decline_of_plane_counter == 1)
            {

                Crash_tune.Play();
                StartCoroutine(stopTimer());
                
               

                decline_of_plane_counter = 0;
               
               
            }


            Planes_Script.gameObject.transform.Rotate(-(Vector3.forward) * 15  * Time.deltaTime);

        }

        if (barr.rectTransform.localScale.x <= 0)

        {
            value_of_fuel_zero = 0;
        }

    }
    IEnumerator timer_for()
    {
        yield return new WaitForSeconds(5f);
        barr.gameObject.LeanRotateX(1, 0.5f);
    }

    void AnimateBar()
    {
        LeanTween.scaleX(bar, 0, Timer);
    }

    public void bar_decrease_sequence()
    {
        barr.gameObject.LeanScaleX(0, 10f);
    }

    public void bar_increase_sequence()
    {
        barr.gameObject.LeanScaleX(1, 1f);
    }


    IEnumerator bar_animate()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        val = 0;
    }

    IEnumerator stopTimer()
    {
        
        
        yield return new WaitForSecondsRealtime(2f);
        Crash_tune.Stop();
        decline_of_plane_counter = 1;
    }

 

}
